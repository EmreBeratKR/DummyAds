using System;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace EmreBeratKR.DummyAds
{
    [CreateAssetMenu(menuName = "DummyAds/Manager")]
    public class DummyAdsManager : ScriptableObject
    {
        private const string Path = "";
        
        
        [Header("Prefabs")]
        [SerializeField] private DummyAdPrefabPair<RewardedDummyAd> rewardedAds;

        [Header("Settings")] 
        [SerializeField] private DummyAdData defaultAdData;
        [SerializeField] private DummyAdData[] adDatas;


        private static readonly NullReferenceException NoManagerException = new NullReferenceException("Could not found any DummyAdsManager in Resources folders!");
        private static readonly Exception MultipleDummyAdsException = new Exception("You can only have one DummyAd at once!");
        
        
        private static DummyAdsManager m_Instance;

        private static DummyAdsManager Instance
        {
            get
            {
                if (m_Instance == null)
                {
                    var instances = Resources.LoadAll<DummyAdsManager>(Path);
                    if (instances.Length <= 0)
                    {
                        throw NoManagerException;
                    }

                    m_Instance = instances[0];
                }

                return m_Instance;
            }
        }

        private static DummyAdData RandomAdData
        {
            get
            {
                var adDatas = Instance.adDatas;

                if (adDatas.Length <= 0) return Instance.defaultAdData;
                
                var randomIndex = Random.Range(0, adDatas.Length);
                return adDatas[randomIndex];
            }
        }

        private static bool HasActiveAd
        {
            get
            {
                var ads = FindObjectsOfType<BaseDummyAd>();
                return ads.Length > 0;
            }
        }


        public static RewardedDummyAd BuildRewardedDummyAd(DummyAdOrientation orientation)
        {
            if (HasActiveAd)
            {
                throw MultipleDummyAdsException;
            }
            
            var prefab = Instance.rewardedAds.Get(orientation);
            var newAd = Object.Instantiate(prefab);
            var randomAdData = RandomAdData;
            var sprite = orientation == DummyAdOrientation.Landscape
                ? randomAdData.landscapeAdSprite
                : randomAdData.portraitAdSprite;
            
            newAd
                .SetAdSprite(sprite)
                .SetTargetUrl(randomAdData.url);

            return newAd;
        }
    }

    [Serializable]
    public struct DummyAdPrefabPair<T>
    {
        [SerializeField] private T 
            landscape, 
            portrait;


        public T Get(DummyAdOrientation orientation)
        {
            switch (orientation)
            {
                case DummyAdOrientation.Landscape:
                    return landscape;
                case DummyAdOrientation.Portrait:
                    return portrait;
                default:
                    throw new ArgumentOutOfRangeException(nameof(orientation), orientation, null);
            }
        }
    }

    [Serializable]
    public struct DummyAdData
    {
        public string name;
        public Sprite landscapeAdSprite;
        public Sprite portraitAdSprite;
        public string url;
    }
}
