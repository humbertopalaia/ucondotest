using AutoMapper;
using ChartAccountBusiness.Interfaces;
using ChartAccountDomain;
using ChartAccountRepository;
using System.Collections.Generic;

namespace ChartAccountBusiness
{
    public class ChartAccountBusiness : IChartAccountBusiness
    {

        private readonly IGenericRepository<ChartAccount> _repository;
        public ChartAccountBusiness(IGenericRepository<ChartAccount> repository) {
            _repository = repository;
        }

        private List<string> ValidateInsert(ChartAccount account)
        {
            return new List<string>();
        }

        public string GetNextCode(string parentCode)
        {
            string newCode = string.Empty;
            var codeParts = parentCode.Split(".");
            var level = codeParts.Length - 1;
            var newSingleCode =  GetNewSingleCode(parentCode, level);

            var changedLevel = newSingleCode.Item1;
            var newCodeValue = newSingleCode.Item2;

            for (int i = 0; i <= changedLevel; i++)
            {
                if (i == changedLevel)
                    newCode += newCodeValue.ToString();
                else
                    newCode += codeParts[i] + ".";                
            }

            var chartAccount = _repository.Get(x => x.Code == newCode);
            
            if(chartAccount.Count() > 0) 
                newCode = GetNextCode(newCode);

            return newCode;
        }


        private Tuple<int,int> GetNewSingleCode(string parentCode, int level)
        {
            var newCodeToReturn = new Tuple<int, int>(0,0);
            var codeParts = parentCode.Split('.');

            var currentCode = Convert.ToInt32(codeParts[level]);

            if (currentCode == 999 && level > 0)
            {
                newCodeToReturn = GetNewSingleCode(parentCode, --level);
            }
            else if (level > 0)
            {
                currentCode++;
                newCodeToReturn = new Tuple<int, int>(level, currentCode++);
            }
            else
                throw new Exception("Code limit exceded, try a new main parent");


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

        public OperationResult Delete(int id, bool autoSave = true)
        {
            _repository.Delete(id);
            return new OperationResult { Success = true };
        }


    }
}