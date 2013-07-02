﻿
using System.IO;
using FluentSharp.ExtensionMethods;
using NUnit.Framework;

namespace UnitTests.FluentSharp_CoreLib
{
    [TestFixture]
    public class Test_IO
    {
        public string TempFile1 { get; set; }
        public string TempFile2 { get; set; }
        [SetUp]
        public void setup()
        {
            TempFile1 = "tempFile".tempFile();
            TempFile1 = "tempFile".tempFile();
            Assert.AreNotEqual(TempFile1,TempFile2);
            Assert.IsFalse(TempFile1.fileExists());
            Assert.IsFalse(TempFile2.fileExists());

        }

        [TearDown]
        public void tearDown()
        {
            TempFile1.file_Delete();
            TempFile2.file_Delete();
            Assert.IsFalse(TempFile1.fileExists());
            Assert.IsFalse(TempFile2.fileExists());
        }


        //IO_ExtensionMethods_FileInfo
        [Test(Description = "Returns true if a file contains the char 0 (usually found on binary files)")]
        public void isBinaryFormat()
        {
            var result_TextFile      = "test".save().isBinaryFormat();
            var result_TextWithChar0 = "aaa\0aaa".saveAs(TempFile1).isBinaryFormat();
            var result_Assembly      = typeof (string).assemblyLocation().isBinaryFormat();

            Assert.IsFalse(result_TextFile);
            Assert.IsTrue (result_TextWithChar0);
            Assert.IsTrue (result_Assembly);            
        }

        [Test(Description = "Returns a FileInfo object for the provided file")]
        public void fileInfo()
        {
            var testContent = "testContent".add_RandomLetters(200);
            testContent.saveAs(TempFile1);
            var fileInfo = TempFile1.fileInfo();
            Assert.IsNotNull(fileInfo);
            Assert.IsInstanceOf<FileInfo>(fileInfo);
            Assert.AreEqual(TempFile1,fileInfo.FullName);            
            Assert.AreEqual(testContent.size(),fileInfo.size());            
            Assert.IsTrue  (fileInfo.Exists);

            var nonExistingFile = 50.randomLetters().fileInfo();
            Assert.IsNotNull(nonExistingFile);
            Assert.IsFalse  (nonExistingFile.Exists);
            //test catch
            
            Assert.IsNull((null as string).fileInfo());
        }

        [Test(Description = "Gets the attributes of the current file")]
        public void file_Attributes()
        {
            fileInfo();
            var fileAttributes1  = TempFile1.fileInfo().Attributes;
            var fileAttributes2  = TempFile1.file_Attributes();
            Assert.AreEqual(fileAttributes1, fileAttributes2);

            Assert.AreEqual(default(FileAttributes), (null as string).file_Attributes());
        }

        [Test(Description = "Adds the ReadOnly attribute from a particular file")]
        public void readOnly_Add()
        {
            var testContent = "testContent".add_RandomLetters(200);
            testContent.saveAs(TempFile1);
            var fileInfo = TempFile1.fileInfo();
            var attributes = fileInfo.attributes();

            Assert.IsTrue (attributes.str().contains("Archive"));
            Assert.IsTrue (TempFile1.file_Has_Attribute(FileAttributes.Archive));
            Assert.IsFalse(TempFile1.file_Has_Attribute(FileAttributes.ReadOnly));

            fileInfo.readOnly_Add();
            Assert.IsTrue(TempFile1.file_Has_Attribute(FileAttributes.ReadOnly));
            var deleteResult = fileInfo.path().file_Delete();
            Assert.IsFalse(deleteResult);
            fileInfo.readOnly_Remove();
            Assert.IsFalse(TempFile1.file_Has_Attribute(FileAttributes.ReadOnly));
        }

        [Test(Description = "Removes the ReadOnly attribute from a particular file")]
        public void readOnly_Remove()
        {
            readOnly_Add();
        }

        [Test(Description = "Returns true if the provided file has a particular attribute")]
        public void file_Has_Attribute()
        {
            //readOnly_Add();
            Assert.IsFalse((null as string).file_Has_Attribute(FileAttributes.Archive));
        }

        [Test(Description = "Gets the attribues of the provided FileInfo object")]
        public void attributes()
        {
            //readOnly_Add();
            
            var attributes1 = TempFile2.fileInfo().attributes();

            Assert.IsFalse(TempFile2.fileExists());
            Assert.AreEqual(attributes1, default(FileAttributes));
        }

        [Test(Description = "Returns the path of the current FileInfo")]
        public void path()
        {
            //readOnly_Add();
            Assert.IsNull((null as string).fileInfo().path());
        }

        [Test(Description = "Removes the readonly attribute from one or more files")]
        public void files_Attribute_ReadOnly_Remove()
        {
            "aaa".saveAs(TempFile1)
                 .file_Attribute_ReadOnly_Add();
            "bbb".saveAs(TempFile2)
                .file_Attribute_ReadOnly_Add();

            var files = new [] {TempFile1, TempFile2}.toList();
            files.files_Attribute_ReadOnly_Remove();
        }


        //IO_ExtensionMethods_Delete_or_Copy
        [Test(Description = "Deletes a file")]
        public void file_Delete()
        {
            "aaaa".saveAs(TempFile1);
            TempFile1.file_Attribute_ReadOnly_Add();
            var deleteResult_ReadOnly = TempFile1.file_Delete();
            TempFile1.file_Attribute_ReadOnly_Remove();
            var deleteResult_NotReadOnly = TempFile1.file_Delete();
            Assert.IsFalse(deleteResult_ReadOnly);
            Assert.IsTrue(deleteResult_NotReadOnly);
        }
    }
}
