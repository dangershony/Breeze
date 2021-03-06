﻿using System;
using System.Collections.Generic;
using Breeze.Wallet.Models;
using NBitcoin;

namespace Breeze.Wallet
{
    /// <summary>
    /// Interface for a manager providing operations on wallets.
    /// </summary>
    public interface IWalletManager : IDisposable
    {
        /// <summary>
        /// Creates a wallet and persist it as a file on the local system.
        /// </summary>
        /// <param name="password">The password used to encrypt sensitive info.</param>
        /// <param name="folderPath">The folder where the wallet will be saved.</param>
        /// <param name="name">The name of the wallet.</param>
        /// <param name="network">The network this wallet is for.</param>
        /// <param name="passphrase">The passphrase used in the seed.</param>
        /// <returns>A mnemonic defining the wallet's seed used to generate addresses.</returns>
        Mnemonic CreateWallet(string password, string folderPath, string name, string network, string passphrase = null);

        /// <summary>
        /// Loads a wallet from a file.
        /// </summary>
        /// <param name="password">The user's password.</param>
        /// <param name="folderPath">The folder where the wallet will be loaded.</param>
        /// <param name="name">The name of the wallet.</param>
        /// <returns>The wallet.</returns>
        Wallet LoadWallet(string password, string folderPath, string name);

        /// <summary>
        /// Recovers a wallet.
        /// </summary>
        /// <param name="password">The user's password.</param>
        /// <param name="folderPath">The folder where the wallet will be loaded.</param>
        /// <param name="name">The name of the wallet.</param>
        /// <param name="network">The network in which to creae this wallet</param>
        /// <param name="mnemonic">The user's mnemonic for the wallet.</param>		
        /// <param name="passphrase">The passphrase used in the seed.</param>
        /// <param name="creationTime">The date and time this wallet was created.</param>
        /// <returns>The recovered wallet.</returns>
        Wallet RecoverWallet(string password, string folderPath, string name, string network, string mnemonic, string passphrase = null, DateTime? creationTime = null);

        /// <summary>
        /// Deletes a wallet.
        /// </summary>
        /// <param name="walletFilePath">The location of the wallet file.</param>        
        void DeleteWallet(string walletFilePath);

        /// <summary>
        /// Gets an account that contains no transactions.
        /// </summary>
        /// <param name="walletName">The name of the wallet from which to get an account.</param>
        /// <param name="coinType">The type of coin for which to get an account.</param>
        /// <param name="password">The password used to decrypt the private key.</param>
        /// <remarks>
        /// According to BIP44, an account at index (i) can only be created when the account
        /// at index (i - 1) contains transactions.
        /// </remarks>
        /// <returns>An unused account.</returns>
        HdAccount GetUnusedAccount(string walletName, CoinType coinType, string password);

        /// <summary>
        /// Gets an account that contains no transactions.
        /// </summary>
        /// <param name="wallet">The wallet from which to get an account.</param>
        /// <param name="coinType">The type of coin for which to get an account.</param>
        /// <param name="password">The password used to decrypt the private key.</param>
        /// <remarks>
        /// According to BIP44, an account at index (i) can only be created when the account
        /// at index (i - 1) contains transactions.
        /// </remarks>
        /// <returns>An unused account.</returns>
        HdAccount GetUnusedAccount(Wallet wallet, CoinType coinType, string password);

        /// <summary>
        /// Creates a new account.
        /// </summary>
        /// <param name="wallet">The wallet in which this account will be created.</param>
        /// <param name="coinType">The type of coin for which to create an account.</param>
        /// <param name="password">The password used to decrypt the private key.</param>
        /// <remarks>
        /// According to BIP44, an account at index (i) can only be created when the account
        /// at index (i - 1) contains transactions.
        /// </remarks>
        /// <returns>The new account.</returns>
        HdAccount CreateNewAccount(Wallet wallet, CoinType coinType, string password);

        /// <summary>
        /// Gets an address that contains no transaction.
        /// </summary>
        /// <param name="walletName">The name of the wallet in which this address is contained.</param>
        /// <param name="coinType">The type of coin for which to get the address.</param>
        /// <param name="accountName">The name of the account in which this address is contained.</param>
        /// <returns>An unused address or a newly created address, in Base58 format.</returns>
        string GetUnusedAddress(string walletName, CoinType coinType, string accountName);

        /// <summary>
        /// Gets a collection of addresses containing transactions for this coin.
        /// </summary>
        /// <param name="walletName">The name of the wallet to get history from.</param>
        /// <param name="coinType">Type of the coin.</param>
        /// <returns></returns>
        IEnumerable<HdAddress> GetHistoryByCoinType(string walletName, CoinType coinType);

        /// <summary>
        /// Gets a collection of addresses containing transactions for this coin.
        /// </summary>
        /// <param name="wallet">The wallet to get history from.</param>
        /// <param name="coinType">Type of the coin.</param>
        /// <returns></returns>
        IEnumerable<HdAddress> GetHistoryByCoinType(Wallet wallet, CoinType coinType);

        WalletGeneralInfoModel GetGeneralInfo(string walletName);

        /// <summary>
        /// Gets a list of accounts filtered by coin type.
        /// </summary>
        /// <param name="walletName">The name of the wallet to look into.</param>
        /// <param name="coinType">The type of coin to filter by.</param>
        /// <returns></returns>
        IEnumerable<HdAccount> GetAccountsByCoinType(string walletName, CoinType coinType);

        WalletBuildTransactionModel BuildTransaction(string password, string address, Money amount, string feeType, bool allowUnconfirmed);

        bool SendTransaction(string transactionHex);

        /// <summary>
        /// Processes a block received from the network.
        /// </summary>
        /// <param name="coinType">The type of coin this block relates to.</param>
        /// <param name="height">The height of the block in the blockchain.</param>
        /// <param name="block">The block.</param>
        void ProcessBlock(CoinType coinType, int height, Block block);

        /// <summary>
        /// Processes a transaction received from the network.
        /// </summary>
        /// <param name="coinType">The type of coin this transaction relates to.</param>
        /// <param name="transaction">The transaction.</param>
        /// <param name="blockHeight">The height of the block this transaction came from. Null if it was not a transaction included in a block.</param>
        /// <param name="blockTime">The block time.</param>
        void ProcessTransaction(CoinType coinType, NBitcoin.Transaction transaction, int? blockHeight = null, uint? blockTime = null);
    }
}
