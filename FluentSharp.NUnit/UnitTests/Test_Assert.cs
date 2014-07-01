﻿using FluentSharp.CoreLib;
using FluentSharp.CoreLib.API;
using FluentSharp.NUnit;
using NUnit.Framework;

namespace UnitTests.FluentSharp_NUnit
{    
    public class Test_Assert : NUnitTests
    {
        [Test(Description = "Asserts that bool is false")]
        public void assert_Is_False()
        {            
            assert_Is_False    (false);
            assert_Is_True     (true);
            assert_Is_Not_False(true);
            assert_Is_Not_True (false);
            
        }

        [Test(Description = "Asserts that an object is Null")]
        public void assert_Is_Null()
        {            
            assert_Is_Null    ((null as string));            
            assert_Is_Not_Null(" aaa ");                            
        }

        [Test(Description = "Asserts that an object is Not Null")]
        public void assert_Is_Not_Null()
        {
            assert_Is_Null();
        }

        [Test(Description = "Asserts that an Directory exists (same as assert_Folder_Exists)")]
        public void assert_Dir_Exists()
        {            
            var dir = PublicDI.config.O2TempDir;
            assert_Dir_Exists (dir);            
        }

        [Test(Description = "Asserts that an Directory doesn't exists (same as assert_Folder_Not_Exists)")]
        public void assert_Dir_Not_Exists()
        {            
            var dir = PublicDI.config.O2TempDir.pathCombine("".add_RandomLetters(10));
            assert_Dir_Not_Exists (dir);            
        }

        [Test(Description = "Asserts that an File exists")]
        public void assert_File_Exists()
        {            
             var file1 = "aaa".tempFile();
            var file2 = "aaa";

            assert_File_Not_Exists(file1);
            assert_File_Not_Exists(file2);

            "123".saveAs(file1);

            assert_File_Exists(file1);
         }

        [Test(Description = "Asserts that an File doesn't exists")]
        public void assert_File_Not_Exists()
        {            
            assert_File_Exists();         
        }
         [Test(Description = "Asserts that an Folder exists (same as assert_Dir_Exists)")]
        public void assert_Folder_Exists()
        {            
            var folder = PublicDI.config.O2TempDir;
            assert_Folder_Exists (folder);      
        }

        [Test(Description = "Asserts that an Folder doesn't exists (same as assert_Dir_Not_Exists)")]
        public void assert_Folder_Not_Exists()
        {            
            var folder = PublicDI.config.O2TempDir.pathCombine("".add_RandomLetters(10));
            assert_Folder_Not_Exists (folder);       
        }

        [Test(Description = "Assert that first value is biggger than second (same as assert_Is_Greater")]
        public void assert_Is_Bigger()
        {
            assert_Is_Bigger(20,10);            
        }
        [Test(Description = "Assert that first value is biggger than second")]
        public void assert_Is_Greater()
        {
            assert_Is_Greater(20,10);
            assert_Is_Greater(20,default(int));
            Assert.Throws<AssertionException>(()=> assert_Is_Greater(10,20));
            Assert.Throws<AssertionException>(()=> assert_Is_Greater(default(int),20));
        }
        [Test(Description = "Assert that first value is biggger than second (same as assert_Is_Less)")]
        public void assert_Is_Smaller()
        {
            assert_Is_Smaller(10,20);            
        }
        [Test(Description = "Assert that first value is biggger than second")]
        public void assert_Is_Less()
        {
            assert_Is_Less(10,20);
            assert_Is_Less(default(int),20);
            Assert.Throws<AssertionException>(()=> assert_Is_Less(20,10));
            Assert.Throws<AssertionException>(()=> assert_Is_Less(20,default(int)));
        }
    }

    public class Test_Assert_ExtensionMethods : NUnitTests
    {
        [Test(Description = "Asserts that bool is false")]
        public void assert_Is_False()
        {            
            false.assert_Is_False    ();
            true .assert_Is_True     ();
            true .assert_Is_Not_False();
            false.assert_Is_Not_True ();            
        }
    }
}
