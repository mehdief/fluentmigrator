#region License
// Copyright (c) 2024, Fluent Migrator Project
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
#endregion

using System.Data;

using FluentMigrator.Runner.Generators.MySql;

using NUnit.Framework;

using Shouldly;

namespace FluentMigrator.Tests.Unit.Generators.MySql4
{
    [TestFixture]
    [Category("MySql")]
    [Category("Generator")]
    [Category("TypeMap")]
    public class MySql4TypeMapTests
    {
        private MySql4TypeMap _typeMap;

        [SetUp]
        public void SetUp()
        {
            _typeMap = new MySql4TypeMap();
        }

        [Test]
        public void UInt16IsUnsignedSmallint()
        {
            _typeMap.GetTypeMap(DbType.UInt16, size: null, precision: null).ShouldBe("UNSIGNED SMALLINT");
        }

        [Test]
        public void UInt32IsUnsignedInteger()
        {
            _typeMap.GetTypeMap(DbType.UInt32, size: null, precision: null).ShouldBe("UNSIGNED INTEGER");
        }

        [Test]
        public void UInt64IsUnsignedBigInt()
        {
            _typeMap.GetTypeMap(DbType.UInt64, size: null, precision: null).ShouldBe("UNSIGNED BIGINT");
        }
    }
}
