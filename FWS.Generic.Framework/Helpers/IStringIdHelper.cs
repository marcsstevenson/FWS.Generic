using FWS.Generic.Framework.Interfaces;

namespace FWS.Generic.Framework.Helpers
{
    public static class IStringIdHelper
    {
        public static void SeedId(this IStringId guidId)
        {
            guidId.Id = GuidComb.Generate().ToString();
        }
    }
}
