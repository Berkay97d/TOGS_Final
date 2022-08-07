using Helpers;

namespace EMRE.Scripts
{
    public class FarmLandUpgrader : Scenegleton<FarmLandUpgrader>
    {
        private FarmLandHub m_SelectedFarmLand;


        public static void Select(FarmLandHub farmLand)
        {
            Instance.m_SelectedFarmLand = farmLand;
        }

        public static void UnlockSelected()
        {
            Instance.m_SelectedFarmLand.Unlock();
        }

        public static void UnlockWorkerSelected()
        {
            Instance.m_SelectedFarmLand.UnlockWorker();
        }
        
        public static void UpgradeSelected()
        {
            Instance.m_SelectedFarmLand.Upgrade();
            FruitUpgrade.UpdateFields(Instance.m_SelectedFarmLand.FarmLand);
        }

        public static void UpgradeGrowth()
        {
            Instance.m_SelectedFarmLand.UpgradeGrowth();
            GrowthSpeedDuration.UpdateFields(Instance.m_SelectedFarmLand.FarmLand);
        }
    }
}