﻿using System;
using System.Diagnostics;
using Microsoft.Extensions.Configuration;
using Web_Api.online.Clients.Interfaces;

namespace Web_Api.online.Clients.Models
{
    public class CoinParameters
    {
        private readonly IConfiguration _configuration;

        #region Constructor

        public CoinParameters(ICoinService coinService,
            IConfiguration configuration,
            string daemonUrl,
            string rpcUsername,
            string rpcPassword,
            string walletPassword,
            short rpcRequestTimeoutInSeconds)
        {
            _configuration = configuration;

            if (!string.IsNullOrWhiteSpace(daemonUrl))
            {
                DaemonUrl = daemonUrl;
                UseTestnet = false; //  this will force the CoinParameters.SelectedDaemonUrl dynamic property to automatically pick the daemonUrl defined above
                IgnoreConfigFiles = true;
                RpcUsername = rpcUsername;
                RpcPassword = rpcPassword;
                WalletPassword = walletPassword;
            }

            if (rpcRequestTimeoutInSeconds > 0)
            {
                RpcRequestTimeoutInSeconds = rpcRequestTimeoutInSeconds;
            }
            else
            {
                short rpcRequestTimeoutTryParse = 0;

                if (short.TryParse(_configuration["RpcRequestTimeoutInSeconds"], out rpcRequestTimeoutTryParse))
                {
                    RpcRequestTimeoutInSeconds = rpcRequestTimeoutTryParse;
                }
            }

            if (IgnoreConfigFiles && (string.IsNullOrWhiteSpace(DaemonUrl) || string.IsNullOrWhiteSpace(RpcUsername) || string.IsNullOrWhiteSpace(RpcPassword)))
            {
                throw new Exception($"One or more required parameters, as defined in {GetType().Name}, were not found in the configuration file!");
            }

            if (IgnoreConfigFiles && Debugger.IsAttached && string.IsNullOrWhiteSpace(WalletPassword))
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("[WARNING] The wallet password is either null or empty");
                Console.ResetColor();
            }

            #region BitcoinCash
            if (coinService is BitcoinCashService)
            {
                if (!IgnoreConfigFiles)
                {
                    DaemonUrl = _configuration["BitcoinCash_DaemonUrl"];
                    DaemonUrlTestnet = _configuration["BitcoinCash_DaemonUrl_Testnet"];
                    RpcUsername = _configuration["BitcoinCash_RpcUsername"];
                    RpcPassword = _configuration["BitcoinCash_RpcPassword"];
                    WalletPassword = _configuration["BitcoinCash_WalletPassword"];
                }

                CoinShortName = "BCH";
                CoinLongName = "BitcoinCash";
                IsoCurrencyCode = "BCH";

                TransactionSizeBytesContributedByEachInput = 148;
                TransactionSizeBytesContributedByEachOutput = 34;
                TransactionSizeFixedExtraSizeInBytes = 10;

                FreeTransactionMaximumSizeInBytes = 1000;
                FreeTransactionMinimumOutputAmountInCoins = 0.01M;
                FreeTransactionMinimumPriority = 57600000;
                FeePerThousandBytesInCoins = 0.0001M;
                MinimumTransactionFeeInCoins = 0.0001M;
                MinimumNonDustTransactionAmountInCoins = 0.0000543M;

                TotalCoinSupplyInCoins = 21000000;
                EstimatedBlockGenerationTimeInMinutes = 10;
                BlocksHighestPriorityTransactionsReservedSizeInBytes = 50000;

                BaseUnitName = "Satoshi";
                BaseUnitsPerCoin = 100000000;
                CoinsPerBaseUnit = 0.00000001M;
            }
            #endregion

            #region Bitcoin

            else if (coinService is BitcoinService)
            {
                if (!IgnoreConfigFiles)
                {
                    DaemonUrl = _configuration["Bitcoin_DaemonUrl"];
                    DaemonUrlTestnet = _configuration["Bitcoin_DaemonUrl_Testnet"];
                    RpcUsername = _configuration["Bitcoin_RpcUsername"];
                    RpcPassword = _configuration["Bitcoin_RpcPassword"];
                    WalletPassword = _configuration["Bitcoin_WalletPassword"];
                }

                CoinShortName = "BTC";
                CoinLongName = "Bitcoin";
                IsoCurrencyCode = "XBT";

                TransactionSizeBytesContributedByEachInput = 148;
                TransactionSizeBytesContributedByEachOutput = 34;
                TransactionSizeFixedExtraSizeInBytes = 10;

                FreeTransactionMaximumSizeInBytes = 1000;
                FreeTransactionMinimumOutputAmountInCoins = 0.01M;
                FreeTransactionMinimumPriority = 57600000;
                FeePerThousandBytesInCoins = 0.0001M;
                MinimumTransactionFeeInCoins = 0.0001M;
                MinimumNonDustTransactionAmountInCoins = 0.0000543M;

                TotalCoinSupplyInCoins = 21000000;
                EstimatedBlockGenerationTimeInMinutes = 10;
                BlocksHighestPriorityTransactionsReservedSizeInBytes = 50000;

                BaseUnitName = "Satoshi";
                BaseUnitsPerCoin = 100000000;
                CoinsPerBaseUnit = 0.00000001M;
            }

            #endregion

            #region Litecoin

            else if (coinService is LitecoinService)
            {
                if (!IgnoreConfigFiles)
                {
                    DaemonUrl = _configuration["Litecoin_DaemonUrl"];
                    DaemonUrlTestnet = _configuration["Litecoin_DaemonUrl_Testnet"];
                    RpcUsername = _configuration["Litecoin_RpcUsername"];
                    RpcPassword = _configuration["Litecoin_RpcPassword"];
                    WalletPassword = _configuration["Litecoin_WalletPassword"];
                }

                CoinShortName = "LTC";
                CoinLongName = "Litecoin";
                IsoCurrencyCode = "XLT";

                TransactionSizeBytesContributedByEachInput = 148;
                TransactionSizeBytesContributedByEachOutput = 34;
                TransactionSizeFixedExtraSizeInBytes = 10;

                FreeTransactionMaximumSizeInBytes = 5000;
                FreeTransactionMinimumOutputAmountInCoins = 0.001M;
                FreeTransactionMinimumPriority = 230400000;
                FeePerThousandBytesInCoins = 0.001M;
                MinimumTransactionFeeInCoins = 0.001M;
                MinimumNonDustTransactionAmountInCoins = 0.001M;

                TotalCoinSupplyInCoins = 84000000;
                EstimatedBlockGenerationTimeInMinutes = 2.5;
                BlocksHighestPriorityTransactionsReservedSizeInBytes = 16000;
                BlockMaximumSizeInBytes = 250000;

                BaseUnitName = "Litetoshi";
                BaseUnitsPerCoin = 100000000;
                CoinsPerBaseUnit = 0.00000001M;
            }

            #endregion

            #region Dogecoin

            else if (coinService is DogecoinService)
            {
                if (!IgnoreConfigFiles)
                {
                    DaemonUrl = _configuration["Dogecoin_DaemonUrl"];
                    DaemonUrlTestnet = _configuration["Dogecoin_DaemonUrl_Testnet"];
                    RpcUsername = _configuration["Dogecoin_RpcUsername"];
                    RpcPassword = _configuration["Dogecoin_RpcPassword"];
                    WalletPassword = _configuration["Dogecoin_WalletPassword"];
                }

                CoinShortName = "DOGE";
                CoinLongName = "Dogecoin";
                IsoCurrencyCode = "XDG";
                TransactionSizeBytesContributedByEachInput = 148;
                TransactionSizeBytesContributedByEachOutput = 34;
                TransactionSizeFixedExtraSizeInBytes = 10;
                FreeTransactionMaximumSizeInBytes = 1; // free txs are not supported from v.1.8+
                FreeTransactionMinimumOutputAmountInCoins = 1;
                FreeTransactionMinimumPriority = 230400000;
                FeePerThousandBytesInCoins = 1;
                MinimumTransactionFeeInCoins = 1;
                MinimumNonDustTransactionAmountInCoins = 0.1M;
                TotalCoinSupplyInCoins = 100000000000;
                EstimatedBlockGenerationTimeInMinutes = 1;
                BlocksHighestPriorityTransactionsReservedSizeInBytes = 16000;
                BlockMaximumSizeInBytes = 500000;
                BaseUnitName = "Koinu";
                BaseUnitsPerCoin = 100000000;
                CoinsPerBaseUnit = 0.00000001M;
            }

            #endregion

            //#region Sarcoin

            //else if (coinService is SarcoinService)
            //{
            //    if (!IgnoreConfigFiles)
            //    {
            //        DaemonUrl = _configuration["Sarcoin_DaemonUrl"];
            //        DaemonUrlTestnet = _configuration["Sarcoin_DaemonUrl_Testnet"];
            //        RpcUsername = _configuration["Sarcoin_RpcUsername"];
            //        RpcPassword = _configuration["Sarcoin_RpcPassword"];
            //        WalletPassword = _configuration["Sarcoin_WalletPassword"];
            //    }

            //    CoinShortName = "SAR";
            //    CoinLongName = "Sarcoin";
            //    IsoCurrencyCode = "SAR";

            //    TransactionSizeBytesContributedByEachInput = 148;
            //    TransactionSizeBytesContributedByEachOutput = 34;
            //    TransactionSizeFixedExtraSizeInBytes = 10;

            //    FreeTransactionMaximumSizeInBytes = 0;
            //    FreeTransactionMinimumOutputAmountInCoins = 0;
            //    FreeTransactionMinimumPriority = 0;
            //    FeePerThousandBytesInCoins = 0.00001M;
            //    MinimumTransactionFeeInCoins = 0.00001M;
            //    MinimumNonDustTransactionAmountInCoins = 0.00001M;

            //    TotalCoinSupplyInCoins = 2000000000;
            //    EstimatedBlockGenerationTimeInMinutes = 1.5;
            //    BlocksHighestPriorityTransactionsReservedSizeInBytes = 50000;

            //    BaseUnitName = "Satoshi";
            //    BaseUnitsPerCoin = 100000000;
            //    CoinsPerBaseUnit = 0.00000001M;
            //}

            //#endregion

            #region Dash

            else if (coinService is DashService)
            {
                if (!IgnoreConfigFiles)
                {
                    DaemonUrl = _configuration["Dash_DaemonUrl"];
                    DaemonUrlTestnet = _configuration["Dash_DaemonUrl_Testnet"];
                    RpcUsername = _configuration["Dash_RpcUsername"];
                    RpcPassword = _configuration["Dash_RpcPassword"];
                    WalletPassword = _configuration["Dash_WalletPassword"];
                }

                CoinShortName = "DASH";
                CoinLongName = "Dash";
                IsoCurrencyCode = "DASH";

                TransactionSizeBytesContributedByEachInput = 148;
                TransactionSizeBytesContributedByEachOutput = 34;
                TransactionSizeFixedExtraSizeInBytes = 10;

                FreeTransactionMaximumSizeInBytes = 1000;
                FreeTransactionMinimumOutputAmountInCoins = 0.0001M;
                FreeTransactionMinimumPriority = 57600000;
                FeePerThousandBytesInCoins = 0.0001M;
                MinimumTransactionFeeInCoins = 0.001M;
                MinimumNonDustTransactionAmountInCoins = 0.0000543M;

                TotalCoinSupplyInCoins = 18900000;
                EstimatedBlockGenerationTimeInMinutes = 2.7;
                BlocksHighestPriorityTransactionsReservedSizeInBytes = 50000;

                BaseUnitName = "Duff";
                BaseUnitsPerCoin = 100000000;
                CoinsPerBaseUnit = 0.00000001M;
            }

            #endregion

            //#region Mogwai
            //else if (coinService is MogwaicoinService)
            //{
            //    if (!IgnoreConfigFiles)
            //    {
            //        DaemonUrl = _configuration["Mogwaicoin_DaemonUrl"];
            //        DaemonUrlTestnet = _configuration["Mogwaicoin_DaemonUrl_Testnet"];
            //        RpcUsername = _configuration["Mogwaicoin_RpcUsername"];
            //        RpcPassword = _configuration["Mogwaicoin_RpcPassword"];
            //        WalletPassword = _configuration["Mogwaicoin_WalletPassword"];
            //    }
            //    CoinShortName = "Mogwaicoin";
            //    CoinLongName = "Mogwaicoin";
            //    IsoCurrencyCode = "MOG";
            //    TransactionSizeBytesContributedByEachInput = 148;
            //    TransactionSizeBytesContributedByEachOutput = 34;
            //    TransactionSizeFixedExtraSizeInBytes = 10;
            //    FreeTransactionMaximumSizeInBytes = 1000;
            //    FreeTransactionMinimumOutputAmountInCoins = 0.0001M;
            //    FreeTransactionMinimumPriority = 57600000;
            //    FeePerThousandBytesInCoins = 0.0001M;
            //    MinimumTransactionFeeInCoins = 0.001M;
            //    MinimumNonDustTransactionAmountInCoins = 0.0000543M;
            //    TotalCoinSupplyInCoins = 50000000;
            //    EstimatedBlockGenerationTimeInMinutes = 2.0;
            //    BlocksHighestPriorityTransactionsReservedSizeInBytes = 50000;
            //    BaseUnitName = "Puff";
            //    BaseUnitsPerCoin = 100000000;
            //    CoinsPerBaseUnit = 0.00000001M;
            //}
            //#endregion

            //#region Smartcash

            //else if (coinService is SmartcashService)
            //{
            //    if (!IgnoreConfigFiles)
            //    {
            //        DaemonUrl = _configuration["Smartcash_DaemonUrl"];
            //        DaemonUrlTestnet = _configuration["Smartcash_DaemonUrl_Testnet"];
            //        RpcUsername = _configuration["Smartcash_RpcUsername"];
            //        RpcPassword = _configuration["Smartcash_RpcPassword"];
            //        WalletPassword = _configuration["Smartcash_WalletPassword"];
            //    }

            //    CoinShortName = "SMART";
            //    CoinLongName = "Smartcash";
            //    IsoCurrencyCode = "SMART";

            //    TransactionSizeBytesContributedByEachInput = 148;
            //    TransactionSizeBytesContributedByEachOutput = 34;
            //    TransactionSizeFixedExtraSizeInBytes = 10;

            //    FreeTransactionMaximumSizeInBytes = 0; // free txs are not supported
            //    FreeTransactionMinimumOutputAmountInCoins = 0;
            //    FreeTransactionMinimumPriority = 0;
            //    FeePerThousandBytesInCoins = 0.0001M;
            //    MinimumTransactionFeeInCoins = 0.001M;
            //    MinimumNonDustTransactionAmountInCoins = 0.00001M;

            //    TotalCoinSupplyInCoins = 5000000000;
            //    EstimatedBlockGenerationTimeInMinutes = 0.916667;
            //    BlocksHighestPriorityTransactionsReservedSizeInBytes = 50000;

            //    BaseUnitName = "Smartoshi";
            //    BaseUnitsPerCoin = 100000000;
            //    CoinsPerBaseUnit = 0.00000001M;
            //}

            //#endregion

            //#region Dallar

            //else if (coinService is DallarService)
            //{
            //    if (!IgnoreConfigFiles)
            //    {
            //        DaemonUrl = _configuration["Dallar_DaemonUrl"];
            //        DaemonUrlTestnet = _configuration["Dallar_DaemonUrl_Testnet"];
            //        RpcUsername = _configuration["Dallar_RpcUsername"];
            //        RpcPassword = _configuration["Dallar_RpcPassword"];
            //        WalletPassword = _configuration["Dallar_WalletPassword"];
            //    }

            //    CoinShortName = "DAL";
            //    CoinLongName = "Dallar";
            //    IsoCurrencyCode = "DAL";

            //    TransactionSizeBytesContributedByEachInput = 148;
            //    TransactionSizeBytesContributedByEachOutput = 34;
            //    TransactionSizeFixedExtraSizeInBytes = 10;

            //    FreeTransactionMaximumSizeInBytes = 1000;
            //    FreeTransactionMinimumOutputAmountInCoins = 0.001M;
            //    FreeTransactionMinimumPriority = 230400000;
            //    FeePerThousandBytesInCoins = 0.001M;
            //    MinimumTransactionFeeInCoins = 0.0001M;
            //    MinimumNonDustTransactionAmountInCoins = 0.001M;

            //    TotalCoinSupplyInCoins = 80000000;
            //    EstimatedBlockGenerationTimeInMinutes = 1.0;
            //    BlocksHighestPriorityTransactionsReservedSizeInBytes = 16000;
            //    BlockMaximumSizeInBytes = 750000;

            //    BaseUnitName = "Allar";
            //    BaseUnitsPerCoin = 100000000;
            //    CoinsPerBaseUnit = 0.00000001M;
            //}

            //#endregion

            //#region Colx

            //else if (coinService is ColxService)
            //{
            //    if (!IgnoreConfigFiles)
            //    {
            //        DaemonUrl = _configuration["Colx_DaemonUrl"];
            //        DaemonUrlTestnet = _configuration["Colx_DaemonUrl_Testnet"];
            //        RpcUsername = _configuration["Colx_RpcUsername"];
            //        RpcPassword = _configuration["Colx_RpcPassword"];
            //        WalletPassword = _configuration["Colx_WalletPassword"];
            //    }
            //    CoinShortName = "COLX";
            //    CoinLongName = "ColossusXT Coin";
            //    IsoCurrencyCode = "COLX";

            //    TransactionSizeBytesContributedByEachInput = 148;
            //    TransactionSizeBytesContributedByEachOutput = 34;
            //    TransactionSizeFixedExtraSizeInBytes = 10;

            //    FreeTransactionMaximumSizeInBytes = 1000;
            //    FreeTransactionMinimumOutputAmountInCoins = 0.0001M;
            //    FreeTransactionMinimumPriority = 57600000;
            //    FeePerThousandBytesInCoins = 0.0001M;
            //    MinimumTransactionFeeInCoins = 0.001M;
            //    MinimumNonDustTransactionAmountInCoins = 0.0000543M;

            //    TotalCoinSupplyInCoins = 18900000;
            //    EstimatedBlockGenerationTimeInMinutes = 2.7;
            //    BlocksHighestPriorityTransactionsReservedSizeInBytes = 50000;

            //    BaseUnitName = "ucolx";
            //    BaseUnitsPerCoin = 100000000;
            //    CoinsPerBaseUnit = 0.00000001M;
            //}

            //#endregion

            //#region Agnostic coin (cryptocoin)

            //else if (coinService is CryptocoinService)
            //{
            //    CoinShortName = "XXX";
            //    CoinLongName = "Generic Cryptocoin Template";
            //    IsoCurrencyCode = "XXX";

            //    //  Note: The rest of the parameters will have to be defined at run-time
            //}

            //#endregion

            #region Uknown coin exception

            else
            {
                throw new Exception("Unknown coin!");
            }

            #endregion

            #region Invalid configuration / Missing parameters

            if (RpcRequestTimeoutInSeconds <= 0)
            {
                throw new Exception("RpcRequestTimeoutInSeconds must be greater than zero");
            }

            if (string.IsNullOrWhiteSpace(DaemonUrl)
                || string.IsNullOrWhiteSpace(RpcUsername)
                || string.IsNullOrWhiteSpace(RpcPassword))
            {
                throw new Exception($"One or more required parameters, as defined in {GetType().Name}, were not found in the configuration file!");
            }

            #endregion
        }

        #endregion

        public string BaseUnitName { get; set; }
        public uint BaseUnitsPerCoin { get; set; }
        public int BlocksHighestPriorityTransactionsReservedSizeInBytes { get; set; }
        public int BlockMaximumSizeInBytes { get; set; }
        public string CoinShortName { get; set; }
        public string CoinLongName { get; set; }
        public decimal CoinsPerBaseUnit { get; set; }
        public string DaemonUrl { private get; set; }
        public string DaemonUrlTestnet { private get; set; }
        public double EstimatedBlockGenerationTimeInMinutes { get; set; }
        public int ExpectedNumberOfBlocksGeneratedPerDay => (int)EstimatedBlockGenerationTimeInMinutes * GlobalConstants.MinutesInADay;
        public decimal FeePerThousandBytesInCoins { get; set; }
        public short FreeTransactionMaximumSizeInBytes { get; set; }
        public decimal FreeTransactionMinimumOutputAmountInCoins { get; set; }
        public int FreeTransactionMinimumPriority { get; set; }
        public bool IgnoreConfigFiles { get; }
        public string IsoCurrencyCode { get; set; }
        public decimal MinimumNonDustTransactionAmountInCoins { get; set; }
        public decimal MinimumTransactionFeeInCoins { get; set; }
        public decimal OneBaseUnitInCoins => CoinsPerBaseUnit;
        public uint OneCoinInBaseUnits => BaseUnitsPerCoin;
        public string RpcPassword { get; set; }
        public short RpcRequestTimeoutInSeconds { get; set; }
        public string RpcUsername { get; set; }
        public string SelectedDaemonUrl => !UseTestnet ? DaemonUrl : DaemonUrlTestnet;
        public ulong TotalCoinSupplyInCoins { get; set; }
        public int TransactionSizeBytesContributedByEachInput { get; set; }
        public int TransactionSizeBytesContributedByEachOutput { get; set; }
        public int TransactionSizeFixedExtraSizeInBytes { get; set; }
        public bool UseTestnet { get; set; }
        public string WalletPassword { get; set; }
    }
}
