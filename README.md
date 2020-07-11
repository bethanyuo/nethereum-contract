# Playing with Smart Contracts using Nethereum
Use Nethereum .NET integration library for Ethereum, thus simplifying the access and smart contract interaction with Ethereum nodes. The exercise will show how to interact with an already deployed contract on the Ethereum Ropsten Testnet.

## Requirements
[Visual Studio Community 2019](https://visualstudio.microsoft.com/vs/)
* Project Type: C# .NET Core Console Application
* [.NET Framework 4.7.2](https://dotnet.microsoft.com/download)

## NuGetPackages
* Nethereum.Contracts
* Nethereum.Web3

## Terminal Setup
Run this command to initialize a new project in your project directory:
```sh
dotnet new console
```
You will end up with a file called Program.cs that looks like this:
```cs
using System;

namespace nethereumProject
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }
}
```
Use this to install NuGet Package Manager for the iMAC:

Go to the `[Extensions]` icon on the left and look for `NuGet Package Manager`. 

1. Click on View -> Command Palette
2. In the search bar, search for Nethereum.
3. You should see Nethereum Add.
4. Select that.
5. Enter Nethereum in the search bar again.
6. Select Nethereum.Contracts and select the latest.
7. Repeat steps 3 through 5.
8. Select Nethereum.Web3

## Implementing the Contract
Import the required namespaces in order to use Nethereum’s Web3 libraries.

Inside the `Program` class, create a class called `ContractService`, which will be the connection to the Ropsten Testnet. Create `3 read-only private` properties for the `Web3`, the `Contract` and for an `Account`, which will be the wallet.
 
Then, create a constructor, which takes 4 parameters: provider of the node, contract address, contract ABI, and a private key for an `account/wallet`.
 
We will need an `HTTPProvider` in order to create our connection to the Ropsten Testnet using [Infura.io](https://infura.io). Add a Main() entry point just below the ContractService class and add the provider variable.
 
In order to get a contract instance of an already deployed contract, we will need its address and `application binary interface`. For exercise’s purpose, deploy a simple contract storing an array of facts through Remix IDE using MetaMask Ropsten as the provider.
 
If you do not have ETHt, use the MetaMask faucet: https://faucet.metamask.io/

Copy the contract’s ABI by compiling the contract and clicking on the `[ABI]` button below the `Compilation Details`.
You may not be able to copy the ABI directly in Visual Studio because the ABI is a JSON Object.
You have to escape all string characters `(“)` by prepending a backslash `(\”)`.

Hint:
First, stringify the JSON in either JavaScript (`JSON.stringify()`) or Python. Then, use the Visual Studio editor’s `CTRL + H` feature to quickly replace these occurrences. Be careful not to replace the quotes in other parts of your program.

Then, deploy the contract using `Injected Web3` while taking note of the address, copy those values in your C# code.

Because the contract owner can only add facts to this contract, export and copy the private key of the account that deployed the contract. Don’t forget to prepend `0x` to the private key.

## Writing to the Smart Contract
Return to the ContractService class. Create a method which takes a string as an argument (a fact) and adds it to the contract. We will send an _asynchronous_ transaction - `method.SendTransactionAsync(from, gas, value, functionInput)` and it will not wait to be mined, just get the transaction hash.
 
In the main function, call the asynchronous method. Then, press `[Start]`.

A terminal will launch and will contain the transaction hash:
```sh
Transaction Hash: 0x012345678890abcdefgh109876543210ijklmnop123qrs
Press Any Key To Exit...
```
Check the transaction hash in in Ropsten Etherscan to confirm if the transaction has succeeded.
 
Try adding a fact using another private key as an account. Hypothesize on what could happen and test it: _RESULT: `FAIL`_
 
## Reading from the Smart Contract
When reading from a Smart Contract, no wallets or private keys are needed. 
In the `ContractService` class, create a method which gets a fact by a given `index` and returns a string with the fact. Get the method of the contract and then make an _asynchronous_ call with the index as parameter, which will return `Task<string>` and get the result.

Run the program:
```
Fact 1: The Times 03/Jan/2009 Chancellor on brink of second bailout for banks
Press Any Key To Exit...
```
 
Finally, create a method which gets `how many facts` are stored in the contract.

Run the program:
```
Stored facts in the contract: 1
Press Any Key To Exit...
```

## Module
MI4: Module 5: E3
