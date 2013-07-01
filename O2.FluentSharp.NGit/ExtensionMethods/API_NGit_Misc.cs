﻿using System;
using NGit;
using NGit.Api;
using NGit.Transport;

namespace FluentSharp.ExtensionMethods
{
    public static class API_NGit_Misc
    {
        public static bool        isGitRepository(this string pathToFolder)
        {
            return pathToFolder.dirExists() && pathToFolder.pathCombine(".git").dirExists();
        }
        public static string      head(this API_NGit nGit)
        {
            try
            {
                var head = nGit.Repository.Resolve(Constants.HEAD);
                return head.notNull() ? head.Name : null;
            }
            catch (Exception ex)
            {
                ex.log();
                return null;
            }
        }
        public static string      git_Folder(this API_NGit nGit)
        {
            return nGit.files_Location().pathCombine(".git");
        }
        public static Repository  repository(this API_NGit nGit)
        {
            if (nGit.notNull())
                return nGit.Repository;
            return null;
        }
        public static Git         git(this API_NGit nGit)
        {
            if (nGit.notNull())
                return nGit.Git;
            return null;
        }
        public static API_NGit    use_Credential(this API_NGit nGit, string username, string password)
        {
            nGit.Credentials = new UsernamePasswordCredentialsProvider(username, password);
            return nGit;
        }
        public static ObjectId    objectId(this String objectId)
        {
            try
            {
                if (objectId.notValid() || objectId == "0")
                    return ObjectId.FromString(NGit_Consts.EMPTY_SHA1);
                return ObjectId.FromString(objectId);
            }
            catch (Exception ex)
            {
                ex.log("String.objectId");
                return null;
            }            
        }
        public static PersonIdent personIdent(this string name, string email)
        {
            return new PersonIdent(name,email);
        }
        public static string      name(this PersonIdent personIdent)
        {
            if (personIdent.notNull())
                return personIdent.GetName();
            return null;
        }
        public static string      email(this PersonIdent personIdent)
        {
            if (personIdent.notNull())
                return personIdent.GetEmailAddress();
            return null;
        }
        public static DateTime    when(this PersonIdent personIdent)
        {
            if (personIdent.notNull())
                return personIdent.GetWhen();
            return default(DateTime);
        }
        public static string      changeBackslashWithForwardSlash(this string targetString)
        {
            return targetString.replace(@"\\", @"/");
        }
        public static string      fixDoubleForwardSlash(this string targetString)
        {
            return targetString.replace(@"//", @"/");
        }
             
    }
}
