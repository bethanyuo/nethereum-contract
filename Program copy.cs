//using System;
using Nethereum.Contracts;
using Nethereum.Hex.HexTypes;
using Nethereum.Web3;
using Nethereum.Web3.Accounts;

namespace Nethereum_Web3
{
    class Program
    {
        class ContractService
        {
            
            private readonly Web3 web3;
            private readonly Contract contract;
            private readonly Account account;

            private static readonly HexBigInteger GAS = new HexBigInteger(4600000);

            public ContractService(string provider, string contractAddress, string abi, string privateKey)
            {
                this.account = new Account(privateKey);
                this.web3 = new Web3(account, provider);
                this.contract = web3.Eth.GetContract(abi, contractAddress);
            }

            public string AddFact(string fact)
            {
                var addFactFunction = contract.GetFunction("add");
                var txHash = addFactFunction.SendTransactionAsync(account.Address, GAS, new HexBigInteger(0), fact)
                                .ConfigureAwait(false)
                                .GetAwaiter()
                                .GetResult();
                return txHash;
            }

            public string GetFact(int index)
            {   
                var getFactFunction = contract.GetFunction("getFact");
                var task = getFactFunction.CallAsync<string>(index);
                var fact = task.Result;

                return fact;
            }

            public int GetTotalFacts()
            {
                var getTotalFacts = contract.GetFunction("count");
                var task = getTotalFacts.CallAsync<int>();

                return task.Result;
            }
        }

        static void Main(string[] args)
        {
            var provider = "https://ropsten.infura.io/v3/'<PROJECT-ID>'";
            var contractAddress = "0x6aB06024E4bc1841C7Aa82D6f0833D29343503a6";
			var privateKey = "0x'<PRIVATE-KEY>'";
            var abi =
              "[{\"inputs\":[{\"internalType\":\"string\",\"name\":\"newFact\",\"type\":\"string\"}],\"name\":\"add\",\"outputs\":[],\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"inputs\":[],\"stateMutability\":\"nonpayable\",\"type\":\"constructor\"},{\"inputs\":[],\"name\":\"count\",\"outputs\":[{\"internalType\":\"uint256\",\"name\":\"\",\"type\":\"uint256\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"uint256\",\"name\":\"index\",\"type\":\"uint256\"}],\"name\":\"getFact\",\"outputs\":[{\"internalType\":\"string\",\"name\":\"\",\"type\":\"string\"}],\"stateMutability\":\"view\",\"type\":\"function\"}]";

            ContractService contractService = new ContractService(provider, contractAddress, abi, privateKey);

            System.Console.WriteLine($"Stored facts in the contract: {contractService.GetTotalFacts()}");
            System.Console.WriteLine("Press Any Key To Exit...");
            System.Console.ReadLine();

            var index = 0;
            System.Console.WriteLine();
            System.Console.WriteLine($"Fact {index + 1}: {contractService.GetFact(index)}");
            System.Console.WriteLine();
            System.Console.WriteLine("Press Any Key To Exit...");
            System.Console.WriteLine();
            System.Console.ReadLine();

            var fact = "The Times 03/Jan/2009 Chancellor on brink of second bailout for banks";
            System.Console.WriteLine();
            System.Console.WriteLine($"Transaction Hash: {contractService.AddFact(fact)}");
            System.Console.WriteLine();
            System.Console.WriteLine("Press Any Key To Exit...");
            System.Console.WriteLine();
            System.Console.ReadLine();
        }
    }
}
