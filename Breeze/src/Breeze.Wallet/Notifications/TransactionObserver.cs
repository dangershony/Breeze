﻿using NBitcoin;
using Stratis.Bitcoin;

namespace Breeze.Wallet.Notifications
{
    /// <summary>
    /// Observer that receives notifications about the arrival of new <see cref="Transaction"/>s.
    /// </summary>
	public class TransactionObserver : SignalObserver<Transaction>
    {
        private readonly CoinType coinType;

        private readonly IWalletManager walletManager;

        public TransactionObserver(CoinType coinType, IWalletManager walletManager)
        {
            this.coinType = coinType;
            this.walletManager = walletManager;            
        }

        /// <summary>
        /// Manages what happens when a new transaction is received.
        /// </summary>
        /// <param name="transaction">The new transaction</param>
        protected override void OnNextCore(Transaction transaction)
        {
            this.walletManager.ProcessTransaction(this.coinType, transaction);
        }
    }
}
