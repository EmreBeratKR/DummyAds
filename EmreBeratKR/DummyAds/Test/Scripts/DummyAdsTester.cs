using UnityEngine;
using UnityEngine.UI;

namespace EmreBeratKR.DummyAds.Test
{
    public class DummyAdsTester : MonoBehaviour
    {
        [SerializeField] private Text coinField;
        [SerializeField] private Text gemField;


        private void Start()
        {
            UpdateCoinField(DummyCollection.CoinCount);
            UpdateGemField(DummyCollection.GemCount);
        }

        private void OnEnable()
        {
            DummyCollection.OnCoinCountChanged += UpdateCoinField;
            DummyCollection.OnGemCountChanged += UpdateGemField;
        }

        private void OnDisable()
        {
            DummyCollection.OnCoinCountChanged -= UpdateCoinField;
            DummyCollection.OnGemCountChanged -= UpdateGemField;
        }


        public void RequestFreeCoin(int count)
        {
            var newRewardedAd = DummyAdsManager
                .BuildRewardedDummyAd(DummyAdOrientation.Portrait);

            newRewardedAd.OnRewardGranted = () =>
            {
                GiveFreeCoin(count);
            };
        }

        public void RequestFreeGem(int count)
        {
            var newRewardedAd = DummyAdsManager
                .BuildRewardedDummyAd(DummyAdOrientation.Portrait);

            newRewardedAd.OnRewardGranted = () =>
            {
                GiveFreeGem(count);
            };
        }


        private void GiveFreeCoin(int count)
        {
            DummyCollection.AddCoin(count);
        }

        private void GiveFreeGem(int count)
        {
            DummyCollection.AddGem(count);
        }

        private void UpdateCoinField(int count)
        {
            coinField.text = count.ToString();
        }

        private void UpdateGemField(int count)
        {
            gemField.text = count.ToString();
        }
    }
}