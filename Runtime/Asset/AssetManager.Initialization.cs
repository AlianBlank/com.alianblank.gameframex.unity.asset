using YooAsset;

namespace GameFrameX.Asset.Runtime
{
    public partial class AssetManager
    {
        /// <summary>
        /// 根据运行模式创建初始化操作数据
        /// </summary>
        /// <returns></returns>
        private InitializationOperation CreateInitializationOperationHandler()
        {
            switch (PlayMode)
            {
                case EPlayMode.EditorSimulateMode:
                {
                    // 编辑器下的模拟模式
                    return InitializeYooAssetEditorSimulateMode();
                }
                case EPlayMode.OfflinePlayMode:
                {
                    // 单机运行模式
                    return InitializeYooAssetOfflinePlayMode();
                }
                case EPlayMode.HostPlayMode:
                {
                    // 联机运行模式
                    return InitializeYooAssetHostPlayMode();
                }
                case EPlayMode.WebPlayMode:
                {
                    // WebGL运行模式
                    return InitializeYooAssetWebPlayMode();
                }
                default:
                {
                    return null;
                }
            }
        }

        private InitializationOperation InitializeYooAssetEditorSimulateMode()
        {
            var initParameters = new EditorSimulateModeParameters();
            initParameters.SimulateManifestFilePath = EditorSimulateModeHelper.SimulateBuild(EDefaultBuildPipeline.BuiltinBuildPipeline.ToString(), DefaultPackageName);

            return _buildinPackage.InitializeAsync(initParameters);
        }

        private InitializationOperation InitializeYooAssetOfflinePlayMode()
        {
            var initParameters = new OfflinePlayModeParameters();
            return _buildinPackage.InitializeAsync(initParameters);
        }

        private InitializationOperation InitializeYooAssetWebPlayMode()
        {
            var initParameters = new WebPlayModeParameters();
            initParameters.BuildinQueryServices = new QueryStreamingAssetsFileServices();
            initParameters.RemoteServices = new RemoteServices(HostServerURL, FallbackHostServerURL);
            return _buildinPackage.InitializeAsync(initParameters);
        }

        private InitializationOperation InitializeYooAssetHostPlayMode()
        {
            var initParameters = new HostPlayModeParameters();
            initParameters.BuildinQueryServices = new QueryStreamingAssetsFileServices();
            initParameters.RemoteServices = new RemoteServices(HostServerURL, FallbackHostServerURL);
            // initParameters.DeliveryQueryServices = new WebDeliveryQueryServices();
            // initParameters.DeliveryLoadServices = new WebDeliveryLoadServices();
            return _buildinPackage.InitializeAsync(initParameters);
        }
    }
}