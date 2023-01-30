using AutoMapper;
using Castle.Core.Internal;
using ChartAccountBusiness.Interfaces;
using ChartAccountDomain;
using ChartAccountRepository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Security.Principal;

namespace ChartAccountBusiness
{
    public class ChartAccountBusiness : IChartAccountBusiness
    {

        private readonly IGenericRepository<ChartAccount> _repository;

        public ChartAccountBusiness()
        {

        }
        public ChartAccountBusiness(IGenericRepository<ChartAccount> repository)
        {
            _repository = repository;
        }

        private List<string> ValidateInsert(ChartAccount account, bool firstLevel = true)
        {
            var errors = new List<string>();

            try
            {
                errors.AddRange(ValidateInsertBasicInfo(account));
                errors.AddRange(ValidateAccountTypes(account));

                if (firstLevel)
                    errors.AddRange(ValidateFirstLevelParent(account));

                errors.AddRange(ValidateChildren(account));
            }
            catch (Exception ex)
            {
                errors.Add(ex.Message);
            }

            return errors;
        }

        private List<string> ValidateInsertBasicInfo(ChartAccount account)
        {
            var errors = new List<string>();


            var codeNumber = account.Code.Replace(".", "");

            if (!long.TryParse(codeNumber, out var code))
                errors.Add("The code has an invalid format");


            if (account.AcceptEntry && account.Children.Count() > 0)
                errors.Add("Chart of accounts that has accept entry can't have children entries");


            var invalidsType = account.Type != "R" && account.Type != "D";
            if (invalidsType)
                errors.Add($"Type of account {account.Name}, must be R or D");

            var codes = account.Code.Split(".");

            foreach (var currentCode in codes)
            {
                if (Convert.ToInt32(currentCode) > 999)
                    errors.Add("The code part can't be more then 999");
            }


            var existCode = _repository.Get(x => x.Code == account.Code).Any();
            if (existCode)
                errors.Add($"The code {account.Code} already exists");

            return errors;

        }

        private List<string> ValidateAccountTypes(ChartAccount account)
        {
            var mainParentCode = account.Code.Split('.').First();
            var parentType = _repository.Get(x => x.Code == mainParentCode).FirstOrDefault().Type;

            List<string> errors = new List<string>();
            
            List<ChartAccount> accountsDiferentType = new List<ChartAccount>();

            if (account.Type != parentType)
                accountsDiferentType.Add(account);

            accountsDiferentType.AddRange(account.Children.Where(x => x.Type != parentType).ToList());
            
            if (accountsDiferentType.Count > 0)
            {
                foreach (var childAccount in accountsDiferentType)
                {
                    errors.Add($"Child account {childAccount.Name} must have been same type of parent account");
                }
            }

            return errors;
        }
        private List<string> ValidateFirstLevelParent(ChartAccount account)
        {

            List<string> errors = new List<string>();


            var parentCode = GetParentCode(account.Code);

            if (!string.IsNullOrEmpty(parentCode))
            {
                var parent = _repository.Get(x => x.Code == parentCode).FirstOrDefault();

                if (parent == null)
                {
                    errors.Add($"Parent from entry {account.Code} does not exists");
                }
                else
                {
                    if (parent.AcceptEntry)
                        errors.Add($"Parent {parentCode} can't have children because him accept entries");
                }
            }

            return errors;
        }
        private List<string> ValidateChildren(ChartAccount entity)
        {
            List<string> errors = new List<string>();

            foreach (var child in entity.Children)
            {
                var parentCode = GetParentCode(child.Code);

                if (parentCode != entity.Code)
                {
                    errors.Add($"The code pattern of child ({child.Code}) isn't the same of parent ({entity.Code})");
                }


                var childErrors = ValidateInsert(child, false);
                errors.AddRange(childErrors);
            }

            return errors;
        }


        public int? GetParentId(string childCode)
        {
            var parentCode = GetParentCode(childCode);

            if (!parentCode.IsNullOrEmpty())
            {
                var parent = _repository.Get(x => x.Code == parentCode).FirstOrDefault();
                return parent.Id;
            }
            else
                return null;
        }

        public string GetParentCode(string code)
        {
            var codes = code.Split('.');
            var parentCode = string.Empty;

            if (codes != null && codes.Length > 1)
            {
                for (int i = 0; i < codes.Length - 1; i++)
                {
                    parentCode += codes[i] + ".";
                }

                parentCode = parentCode.Substring(0, parentCode.Length - 1);
            }

            return parentCode;
        }

        public string GetNextCode(string parentCode)
        {
            string newCode = string.Empty;
            var chartAccount = _repository.Get(x => x.Code == parentCode, null, "Children").FirstOrDefault();
            var lastCode = chartAccount.Children.OrderByDescending(x => x.LevelCode).FirstOrDefault();

            var newLastCode = 0;

            if (lastCode == null)
                newLastCode = 1;
            else
                newLastCode = lastCode.LevelCode+1;


            if (newLastCode > 999)
            {
                //Como o level atual é 999, sobe um nível para procurar o próximo pai disponível
                var codeParts = parentCode.Split(".");
                parentCode = string.Empty;
                for (int i = 0; i < codeParts.Length - 1; i++)
                {
                    parentCode += codeParts[i] + ".";
                }


                //First level, find the last of this level and increment by one, if 999 is achived, throw exception
                if (parentCode.Length == 0)
                {
                    var lastFirstLevel = _repository.Get(x => x.ParentAccountId == null).Max(c => c.LevelCode);
                    lastFirstLevel++;

                    if (lastFirstLevel > 999)
                        throw new Exception("Limit of chart accounts achieved");

                    newCode = lastFirstLevel.ToString();
                }
                else
                {
                    parentCode = parentCode.Substring(0, parentCode.Length - 1);
                    newCode = GetNextCode(parentCode);
                }
            }
            else
            {
                var numberParentCode = newLastCode;

                newCode = parentCode + "." + numberParentCode.ToString();

                var chartAccounts = _repository.Get(x => x.Code == newCode);

            }

            return newCode;

        }


        private Tuple<int, int> GetNewSingleCode(string parentCode, int level)
        {
            var newCodeToReturn = new Tuple<int, int>(0, 0);

            var codeParts = parentCode.Split('.');


            var currentCode = Convert.ToInt32(codeParts[level]);


            if (currentCode == 999)
            {
                if (level > 0)
                    level--;

                newCodeToReturn = GetNewSingleCode(parentCode, level);
            }
            else
            {
                currentCode++;
                newCodeToReturn = new Tuple<int, int>(level, currentCode);
            }


            return newCodeToReturn;
        }





        public ChartAccount GetById(int id)
        {
            return _repository.GetById(id);
        }

        public OperationResult Insert(ChartAccount entity, bool autoSave = true)
        {
            var result = new OperationResult();

            var validations = ValidateInsert(entity);


            if (validations.Count > 0)
                result = new OperationResult { Success = false, Errors = validations };
            else
            {
                var parentId = GetParentId(entity.Code);
                entity.ParentAccountId = parentId;

                _repository.Insert(entity);
                result.Success = true;
            }

            return result;
        }

        public OperationResult Update(ChartAccount entity, bool autoSave = true)
        {
            _repository.Update(entity);
            return new OperationResult { Success = true };
        }

        public OperationResult Delete(string code, bool autoSave = true)
        {
            var entity = _repository.Get(x => x.Code == code, null, "Children").FirstOrDefault();

            foreach (var child in entity.Children)
            {
                Delete(child.Code);
            }


            entity = _repository.Get(x => x.Code == code).FirstOrDefault();

            if (entity != null)
                _repository.Delete(entity.Id);

            return new OperationResult { Success = true };
        }

        public List<ChartAccount> GetAll()
        {

            var all = _repository.GetAll()
                .OrderBy(x => x.Code).OrderBy(x => x.LevelCode)
                .ToList();

            return all.Where(x => x.ParentAccountId == null).ToList();
        }
    }
}