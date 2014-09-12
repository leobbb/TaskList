﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TaskList;

namespace TaskListTest
{
    [TestClass]
    public class UnitTest1
    {
        // 用于访问测试对象的私有成员的类对象
        private PrivateObject privateObject;
        private PrivateType privateType;


        [TestInitialize]
        public void TestInitialize()
        {
            // 使用测试对象的类型，实例化
            privateObject = new PrivateObject(typeof(xmlForm));
            privateType = new PrivateType(typeof(xmlForm));

            // 每个测试方法开始之前，对私有字段 xDoc 赋值
            System.Xml.XmlDocument xDoc = new System.Xml.XmlDocument();
            xDoc.Load("Data/TaskList.xml");
            privateObject.SetField("xDoc", xDoc);

            
            //privateObject.Invoke("xmlForm_Load", new System.Object(), new System.EventArgs());
        }

        [TestMethod]
        public void TestRefreshList()
        {
            // Arrange
            //bool expected = true;
            string str1 = "done", str2 = "doing";

            // Act 
            // 由于 PrivateClass 使用了反射，这里必须从通用 Object 类型转换成正确的类型
            bool actual1 = (bool)privateObject.Invoke("refreshList", str1);
            bool actual2 = (bool)privateObject.Invoke("refreshList", str2);

            // Assert 
            Assert.IsTrue(actual1, "done's wrong");
            Assert.IsTrue(actual2, "doing's wrong");
        }

        [TestMethod]
        public void TestCreateXml()
        {
            // Arrange
            string str = "Data/TaskList.xml";
            if (System.IO.File.Exists(str))
                System.IO.File.Delete(str);
            
            privateType.SetStaticField("path", str);
            
            // Act
            privateObject.Invoke("createNewXmlFile");

            // Assert 
            if (!System.IO.File.Exists(str))
                Assert.Fail("文件没有保存成功");
        }

        [TestMethod]
        public void TestStaticFieldCount()
        {
            // Arrange 
            // 只要能获取到静态字段 count 的值 就可以

            // Act 
            int actual = (int)privateType.GetStaticField("count");

            // Assert 
            Assert.IsTrue(actual > -1);
        }

        [TestMethod]
        public void TestValidXml()
        {            

            bool autual = (bool)privateObject.Invoke("ValidXml", "Data/TaskList.xml");

            Assert.IsTrue(autual);
        }

        [TestMethod]
        public void TestAddTask()
        {
            TaskList.Task task = new Task(88, "测试测试");

            bool actual = (bool)privateObject.Invoke("addXmlElement", task);

            Assert.IsTrue(actual); 
        }
    }
}
