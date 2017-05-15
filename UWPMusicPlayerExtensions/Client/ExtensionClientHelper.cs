using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UWPMusicPlayerExtensions.Enums;
using Windows.ApplicationModel.AppExtensions;
using Windows.ApplicationModel.AppService;
using Windows.Foundation.Collections;

namespace UWPMusicPlayerExtensions.Client
{
    public class ExtensionClientHelper : IExtensionClientHelper
    {
        private ExtensionTypes extensionType;
        private string appExtensionName;
        private AppExtensionCatalog catalog;
        private List<AppExtensionInfo> extensions;

        public ExtensionClientHelper(ExtensionTypes extensionType)
        {
            this.extensionType = extensionType;
            appExtensionName = ExtensionsNames.GetName(extensionType);
            catalog = AppExtensionCatalog.Open(appExtensionName);
            catalog.PackageInstalled += Catalog_PackageInstalled;
            catalog.PackageUninstalling += Catalog_PackageUninstalling;
        }

        private void Catalog_PackageUninstalling(AppExtensionCatalog sender, AppExtensionPackageUninstallingEventArgs args)
        {
            extensions = null;
        }

        private void Catalog_PackageInstalled(AppExtensionCatalog sender, AppExtensionPackageInstalledEventArgs args)
        {
            extensions = null;
        }

        public async Task<List<AppExtensionInfo>> GetAvailableExtensions()
        {
            if (extensions == null)
            {
                extensions = new List<AppExtensionInfo>();
                foreach (var extension in await catalog.FindAllAsync())
                {
                    var properties = await extension.GetExtensionPropertiesAsync() as PropertySet;

                    if (properties != null && properties.ContainsKey("Service"))
                    {
                        PropertySet service = properties["Service"] as PropertySet;
                        extensions.Add(new AppExtensionInfo()
                        {
                            DisplayName = extension.DisplayName,
                            Id = extension.Id,
                            PackageName = extension.Package.Id.FamilyName,
                            ServiceName = service["#text"].ToString(),
                            Type = extensionType,
                        });
                    }
                }
            }
            return extensions;
        }

        public async Task<ValueSet> InvokeExtension(AppExtensionInfo extensionInfo, Dictionary<string, object> parameters)
        {
            var message = new ValueSet();
            foreach (var kv in parameters)
            {
                message.Add(kv);
            }

            var serviceConnection = new AppServiceConnection();
            serviceConnection.AppServiceName = extensionInfo.ServiceName;
            serviceConnection.PackageFamilyName = extensionInfo.PackageName;
            var connectionStatus = await serviceConnection.OpenAsync();

            using (serviceConnection)
            {
                if (connectionStatus != AppServiceConnectionStatus.Success)
                {
                    System.Diagnostics.Debug.WriteLine("InvokeExtension connectionStatus:{0}", connectionStatus);
                    return null;
                }

                var response = await serviceConnection.SendMessageAsync(message);
                if (response.Status == AppServiceResponseStatus.Success)
                {
                    string messageStatus = response.Message[Response.Status] as string;
                    if (messageStatus == Response.OK)
                    {
                        return response.Message;
                    }
                    else
                    {
                        System.Diagnostics.Debug.WriteLine("InvokeExtension status:{0}", messageStatus);
                        return null;
                    }
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("InvokeExtension status:{0}", response.Status);
                    return null;
                }
            }
        }
    }
}
