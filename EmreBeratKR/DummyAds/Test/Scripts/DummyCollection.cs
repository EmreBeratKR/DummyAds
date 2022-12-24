using UnityEngine;
using UnityEngine.Events;

namespace EmreBeratKR.DummyAds.Test
{
    public static class DummyCollection
    {
        private const string DummyCoinSaveKey = "Dummy_Coin";
        private const string DummyGemSaveKey = "Dummy_Gem";


        public static UnityAction<int> OnCoinCountChanged;
        public static UnityAction<int> OnGemCountChanged;


        public static int CoinCount
        {
            get => PlayerPrefs.GetInt(DummyCoinSaveKey, 0);
            private set => PlayerPrefs.SetInt(DummyCoinSaveKey, value);
        }
        
        public static int GemCount
        {
            get => PlayerPrefs.GetInt(DummyGemSaveKey, 0);
            private set => PlayerPrefs.SetInt(DummyGemSaveKey, value);
        }


        public static void AddCoin(int count)
        {
            CoinCount += count;
            OnCoinCountChanged?.Invoke(CoinCount);
        }

        public static void AddGem(int count)
        {
            GemCount += count;
            OnGemCountChanged?.Invoke(GemCount);
        }
    }
}
