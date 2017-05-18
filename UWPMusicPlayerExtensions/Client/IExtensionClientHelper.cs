using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Foundation.Collections;

namespace UWPMusicPlayerExtensions.Client
{
    public interface IExtensionClientHelper
    {
        Task<List<AppExtensionInfo>> GetInstalledExtensions();
        Task<ValueSet> InvokeExtension(AppExtensionInfo info, Dictionary<string, object> parameters);
    }
}
