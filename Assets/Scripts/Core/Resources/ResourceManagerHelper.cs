namespace SFramework.Core
{
    public static class ResourceManagerHelper
    {
        /// <summary>
        /// 获取资源管理器
        /// </summary>
        /// <param name="resourceMode"></param>
        /// <returns></returns>
        public static IResource GetResourceManager(ResourceMode resourceMode)
        {
            switch (resourceMode)
            {
                case ResourceMode.Editor:
                    return null;
                case ResourceMode.Updatable:
                    return new ResourceManager_AB();

            }
            return null;
        }
    }
}