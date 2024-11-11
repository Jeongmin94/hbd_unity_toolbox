using UnityEngine;
using NUnit.Framework;
using ToolBox.Singleton;

using System;

namespace ToolBoxTests
{
    internal class TestSingleton : Singleton<TestSingleton>
    {
        private DateTime _sessionStartTime;
        private DateTime _sessionEndTime;

        private void Start()
        {
            _sessionStartTime = DateTime.Now;

            Debug.Log($"TestSingleton Start @: {_sessionStartTime}");
        }

        private void OnApplicationQuit()
        {
            _sessionEndTime = DateTime.Now;

            TimeSpan timeDifference =
                _sessionEndTime.Subtract(_sessionStartTime);

            Debug.Log(
                "TestSingleton ended @: " + DateTime.Now);
            Debug.Log(
                "TestSingleton lasted: " + timeDifference);
        }
    }


    public class SingletonTest
    {
        private GameObject testObject;


        [SetUp]
        public void SetUpTest()
        {
            testObject = new GameObject();
        }

        [TearDown]
        public void TearDown()
        {
            GameObject.Destroy(testObject);
        }

        [Test]
        public void Singleton_TestUniqueInstance()
        {
            var instance1 = TestSingleton.Instance;
            var instance2 = TestSingleton.Instance;

            Assert.AreSame(instance1, instance2, "Singleton Instance is not unique.");
        }

        [Test]
        public void Singleton_TestAwakeMethod()
        {
            var instance = TestSingleton.Instance;
            Assert.IsNotNull(instance, "Instance is null.");
            Assert.AreEqual(instance.gameObject.name, "TestSingleton");
        }

        [Test]
        public void Singleton_DontDestroyOnLoad()
        {
            var instance = TestSingleton.Instance;

            // `DontDestroyOnLoad`�� ȣ��� ������Ʈ�� �θ� null�� �˴ϴ�.
            Assert.IsNull(instance.transform.parent, "Object parent is not null, `DontDestroyOnLoad` was not called.");
        }

    }
}
