namespace NPS
{
    public static partial class Paths
    {
        public static partial class ResourcePaths
        {
            public static string manifestPath = @"Data/Manifests";
        }

        public static partial class EditorPaths
        {
            public static string manifestPath = @"Assets/Shared/Resources/Data/Manifests";

            public static string ToResourcePath(string _editorPath)
            {
                int pathStart = _editorPath.IndexOf("Resources") + "Resources".Length + 1;
                return _editorPath.Substring(pathStart);
            }
        }
    }
}