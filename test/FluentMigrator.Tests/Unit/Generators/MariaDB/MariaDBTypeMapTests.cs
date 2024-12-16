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

namespace FluentMigrator.Tests.Unit.Generators.MariaDB
{
    [TestFixture]
    [Category("MariaDB")]
    [Category("Generator")]
    [Category("TypeMap")]
    public class MariaDBTypeMapTests
    {
        private MariaDBTypeMap _typeMap;

        [SetUp]
        public void SetUp()
        {
            _typeMap = new MariaDBTypeMap();
        }

        [Test]
        public void ItMapsUInt16ToUnsignedSmallint()
        {
            _typeMap.GetTypeMap(DbType.UInt16, size: null, precision: null).ShouldBe("SMALLINT UNSIGNED");
        }

        [Test]
        public void ItMapsUInt32ToUnsignedInt()
        {
            _typeMap.GetTypeMap(DbType.UInt32, size: null, precision: null).ShouldBe("INT UNSIGNED");
        }

        [Test]
        public void ItMapsUInt64ToUnsignedBigInt()
        {
            _typeMap.GetTypeMap(DbType.UInt64, size: null, precision: null).ShouldBe("BIGINT UNSIGNED");
        }

        [Test]
        public void ItMapsTimeWithoutSizeToTimeWithZeroPrecision()
        {
            _typeMap.GetTypeMap(DbType.Time, size: null, precision: null).ShouldBe("TIME(0)");
        }

        [Test]
        public void ItMapsTimeWithSizeToTimeWithPrecision()
        {
            _typeMap.GetTypeMap(DbType.Time, size: 3, precision: null).ShouldBe("TIME(3)");
        }

        [Test]
        public void ItMapsAnsiStringFixedLengthWithoutSizeToChar255()
        {
            _typeMap.GetTypeMap(DbType.AnsiStringFixedLength, size: null, precision: null).ShouldBe("CHAR(255) CHARACTER SET utf8mb3");
        }

        [Test]
        public void ItMapsAnsiStringFixedLengthWithSizeToCharWithSize()
        {
            _typeMap.GetTypeMap(DbType.AnsiStringFixedLength, size: 255, precision: null).ShouldBe("CHAR(255) CHARACTER SET utf8mb3");
        }

        [Test]
        public void ItMapsAnsiStringFixedLengthToText()
        {
            _typeMap.GetTypeMap(DbType.AnsiStringFixedLength, size: 65535, precision: null).ShouldBe("TEXT CHARACTER SET utf8mb3");
        }

        [Test]
        public void ItMapsAnsiStringFixedLengthToMediumText()
        {
            _typeMap.GetTypeMap(DbType.AnsiStringFixedLength, size: 16777215, precision: null).ShouldBe("MEDIUMTEXT CHARACTER SET utf8mb3");
        }

        [Test]
        public void ItMapsAnsiStringFixedLengthToLongText()
        {
            _typeMap.GetTypeMap(DbType.AnsiStringFixedLength, size: int.MaxValue, precision: null).ShouldBe("LONGTEXT CHARACTER SET utf8mb3");
        }

        [Test]
        public void ItMapsAnsiStringWithoutSizeToVarChar255()
        {
            _typeMap.GetTypeMap(DbType.AnsiString, size: null, precision: null).ShouldBe("VARCHAR(255) CHARACTER SET utf8mb3");
        }

        [Test]
        public void ItMapsAnsiStringWithSizeToVarCharWithSize()
        {
            _typeMap.GetTypeMap(DbType.AnsiString, size: 16383, precision: null).ShouldBe("VARCHAR(16383) CHARACTER SET utf8mb3");
        }

        [Test]
        public void ItMapsAnsiStringToText()
        {
            _typeMap.GetTypeMap(DbType.AnsiString, size: 65535, precision: null).ShouldBe("TEXT CHARACTER SET utf8mb3");
        }

        [Test]
        public void ItMapsAnsiStringToMediumText()
        {
            _typeMap.GetTypeMap(DbType.AnsiString, size: 16777215, precision: null).ShouldBe("MEDIUMTEXT CHARACTER SET utf8mb3");
        }

        [Test]
        public void ItMapsAnsiStringToLongText()
        {
            _typeMap.GetTypeMap(DbType.AnsiString, size: int.MaxValue, precision: null).ShouldBe("LONGTEXT CHARACTER SET utf8mb3");
        }

        [Test]
        public void ItMapsBinaryWithoutSizeToLongBlob()
        {
            _typeMap.GetTypeMap(DbType.Binary, size: null, precision: null).ShouldBe("LONGBLOB");
        }

        [Test]
        public void ItMapsBinaryToTinyBlob()
        {
            _typeMap.GetTypeMap(DbType.Binary, size: 255, precision: null).ShouldBe("TINYBLOB");
        }

        [Test]
        public void ItMapsBinaryToBlob()
        {
            _typeMap.GetTypeMap(DbType.Binary, size: 65535, precision: null).ShouldBe("BLOB");
        }

        [Test]
        public void ItMapsBinaryToMediumBlob()
        {
            _typeMap.GetTypeMap(DbType.Binary, size: 16777215, precision: null).ShouldBe("MEDIUMBLOB");
        }

        [Test]
        public void ItMapsBinaryToLongBlob()
        {
            _typeMap.GetTypeMap(DbType.Binary, size: int.MaxValue, precision: null).ShouldBe("LONGBLOB");
        }

        [Test]
        public void ItMapsBooleanToBit()
        {
            _typeMap.GetTypeMap(DbType.Boolean, size: null, precision: null).ShouldBe("BIT");
        }

        [Test]
        public void ItMapsByteToUnsignedTinyInt1()
        {
            _typeMap.GetTypeMap(DbType.Byte, size: null, precision: null).ShouldBe("TINYINT(1) UNSIGNED");
        }

        [Test]
        public void ItMapsCurrencyToDecimal194()
        {
            _typeMap.GetTypeMap(DbType.Currency, size: null, precision: null).ShouldBe("DECIMAL(19,4)");
        }

        [Test]
        public void ItMapsDateToDate()
        {
            _typeMap.GetTypeMap(DbType.Date, size: null, precision: null).ShouldBe("DATE");
        }

        [Test]
        public void ItMapsDateTimeWithoutMicrosecondPrecisionToDateTimeWithZeroMicrosecondPrecision()
        {
            _typeMap.GetTypeMap(DbType.DateTime, size: null, precision: null).ShouldBe("DATETIME(0)");
        }

        [Test]
        public void ItMapsDateTimeWithMicrosecondPrecisionToDateTimeWithMicrosecondPrecision()
        {
            _typeMap.GetTypeMap(DbType.DateTime, size: 6, precision: null).ShouldBe("DATETIME(6)");
        }

        [Test]
        public void ItMapsDateTime2ToDateTimeWithMicrosecondPrecision6()
        {
            _typeMap.GetTypeMap(DbType.DateTime2, size: null, precision: null).ShouldBe("DATETIME(6)");
        }

        [Test]
        public void ItMapsDecimalWithoutSizeToDecimal195()
        {
            _typeMap.GetTypeMap(DbType.Decimal, size: null, precision: null).ShouldBe("DECIMAL(19,5)");
        }

        [Test]
        public void ItMapsDecimalWithSizeToDecimalWithSize()
        {
            _typeMap.GetTypeMap(DbType.Decimal, size: 20, precision: 5).ShouldBe("DECIMAL(20,5)");
        }

        [Test]
        public void ItMapsDoubleToDouble()
        {
            _typeMap.GetTypeMap(DbType.Double, size: null, precision: null).ShouldBe("DOUBLE");
        }

        [Test]
        public void ItMapsInt16ToSmallInt()
        {
            _typeMap.GetTypeMap(DbType.Int16, size: null, precision: null).ShouldBe("SMALLINT");
        }

        [Test]
        public void ItMapsInt32ToInt()
        {
            _typeMap.GetTypeMap(DbType.Int32, size: null, precision: null).ShouldBe("INT");
        }

        [Test]
        public void ItMapsInt64ToBigInt()
        {
            _typeMap.GetTypeMap(DbType.Int64, size: null, precision: null).ShouldBe("BIGINT");
        }

        [Test]
        public void ItMapsSingleToFloat()
        {
            _typeMap.GetTypeMap(DbType.Single, size: null, precision: null).ShouldBe("FLOAT");
        }

        [Test]
        public void ItMapsStringFixedLengthWithoutSizeToChar255()
        {
            _typeMap.GetTypeMap(DbType.StringFixedLength, size: null, precision: null).ShouldBe("CHAR(255) CHARACTER SET utf8mb4");
        }

        [Test]
        public void ItMapsStringFixedLengthWithSizeToCharWithSize()
        {
            _typeMap.GetTypeMap(DbType.StringFixedLength, size: 255, precision: null).ShouldBe("CHAR(255) CHARACTER SET utf8mb4");
        }

        [Test]
        public void ItMapsStringFixedLengthToText()
        {
            _typeMap.GetTypeMap(DbType.StringFixedLength, size: 65535, precision: null).ShouldBe("TEXT CHARACTER SET utf8mb4");
        }

        [Test]
        public void ItMapsStringFixedLengthToMediumText()
        {
            _typeMap.GetTypeMap(DbType.StringFixedLength, size: 16777215, precision: null).ShouldBe("MEDIUMTEXT CHARACTER SET utf8mb4");
        }

        [Test]
        public void ItMapsStringFixedLengthToLongText()
        {
            _typeMap.GetTypeMap(DbType.StringFixedLength, size: int.MaxValue, precision: null).ShouldBe("LONGTEXT CHARACTER SET utf8mb4");
        }

        [Test]
        public void ItMapsStringWithoutSizeToVarChar255()
        {
            _typeMap.GetTypeMap(DbType.String, size: null, precision: null).ShouldBe("VARCHAR(255) CHARACTER SET utf8mb4");
        }

        [Test]
        public void ItMapsStringWithSizeToVarCharWithSize()
        {
            _typeMap.GetTypeMap(DbType.String, size: 16383, precision: null).ShouldBe("VARCHAR(16383) CHARACTER SET utf8mb4");
        }

        [Test]
        public void ItMapsStringToText()
        {
            _typeMap.GetTypeMap(DbType.String, size: 65535, precision: null).ShouldBe("TEXT CHARACTER SET utf8mb4");
        }

        [Test]
        public void ItMapsStringToMediumText()
        {
            _typeMap.GetTypeMap(DbType.String, size: 16777215, precision: null).ShouldBe("MEDIUMTEXT CHARACTER SET utf8mb4");
        }

        [Test]
        public void ItMapsStringToLongText()
        {
            _typeMap.GetTypeMap(DbType.String, size: int.MaxValue, precision: null).ShouldBe("LONGTEXT CHARACTER SET utf8mb4");
        }

        [Test]
        public void ItMapsGuidToUuid()
        {
            _typeMap.GetTypeMap(DbType.Guid, size: null, precision: null).ShouldBe("UUID");
        }
    }
}
