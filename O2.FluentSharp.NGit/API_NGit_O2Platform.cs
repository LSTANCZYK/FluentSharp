// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using FluentSharp.ExtensionMethods;
using O2.Kernel;

namespace FluentSharp
{
    public class API_NGit_O2Platform : API_NGit
    {
        public string GitHub_O2_Repositories { get; set; }
        public string LocalGitRepositories { get; set; }

        public API_NGit_O2Platform()
        {
            LocalGitRepositories = PublicDI.config.CurrentExecutableDirectory.pathCombine("..\\..").fullPath(); // by default it is two above the current one
            GitHub_O2_Repositories = "https://github.com/o2platform/{0}.git";
        }

        public API_NGit_O2Platform(string targetPath) : this()
        {
            LocalGitRepositories = targetPath;
        }
    }

    public static class API_NGit_O2Platform_ExtensionMethods
    {
        public static string repositoryUrl(this API_NGit_O2Platform nGit_O2, string repositoryName)
        {
            return nGit_O2.GitHub_O2_Repositories.format(repositoryName);
        }

        public static API_NGit_O2Platform cloneOrPull(this API_NGit_O2Platform nGit_O2, string repositoryName)
        {
            var repositoryUrl = nGit_O2.repositoryUrl(repositoryName);
            var localPath = nGit_O2.LocalGitRepositories.pathCombine(repositoryName);
            nGit_O2.open_or_Clone(repositoryUrl, localPath);
            return nGit_O2;
        }

        public static API_NGit nGit(this API_NGit_O2Platform nGit_O2)
        {
            return nGit_O2;
        }        
    }
}