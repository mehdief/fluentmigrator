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

using FluentMigrator.Runner.Generators.Base;

namespace FluentMigrator.Runner.Generators.MySql
{
    public class MariaDBTypeMap : TypeMapBase, IMariaDBTypeMap
    {
        public const string AnsiCharSet = "utf8mb3";
        public const int StringCapacity = 255;
        public const int VarcharCapacity = 16383;
        public const int TextCapacity = 65535;
        public const int MediumTextCapacity = 16777215;
        public const int LongTextCapacity = int.MaxValue;
        public const int DecimalCapacity = 65;

        public MariaDBTypeMap()
        {
            SetupTypeMaps();
        }

        protected virtual void SetupMariaDBTypeMaps()
        {
            SetTypeMap(DbType.AnsiStringFixedLength, "CHAR(255) CHARACTER SET " + AnsiCharSet);
            SetTypeMap(DbType.AnsiStringFixedLength, "CHAR($size) CHARACTER SET " + AnsiCharSet, StringCapacity);
            SetTypeMap(DbType.AnsiStringFixedLength, "TEXT CHARACTER SET " + AnsiCharSet, TextCapacity);
            SetTypeMap(DbType.AnsiStringFixedLength, "MEDIUMTEXT CHARACTER SET " + AnsiCharSet, MediumTextCapacity);
            SetTypeMap(DbType.AnsiStringFixedLength, "LONGTEXT CHARACTER SET " + AnsiCharSet, LongTextCapacity);
            SetTypeMap(DbType.AnsiString, "VARCHAR(255) CHARACTER SET " + AnsiCharSet);
            SetTypeMap(DbType.AnsiString, "VARCHAR($size) CHARACTER SET " + AnsiCharSet, VarcharCapacity);
            SetTypeMap(DbType.AnsiString, "TEXT CHARACTER SET " + AnsiCharSet, TextCapacity);
            SetTypeMap(DbType.AnsiString, "MEDIUMTEXT CHARACTER SET " + AnsiCharSet, MediumTextCapacity);
            SetTypeMap(DbType.AnsiString, "LONGTEXT CHARACTER SET " + AnsiCharSet, LongTextCapacity);
            SetTypeMap(DbType.Binary, "LONGBLOB");
            SetTypeMap(DbType.Binary, "TINYBLOB", StringCapacity);
            SetTypeMap(DbType.Binary, "BLOB", TextCapacity);
            SetTypeMap(DbType.Binary, "MEDIUMBLOB", MediumTextCapacity);
            SetTypeMap(DbType.Binary, "LONGBLOB", LongTextCapacity);
            SetTypeMap(DbType.Boolean, "BIT");
            SetTypeMap(DbType.Byte, "TINYINT(1) UNSIGNED");
            SetTypeMap(DbType.Currency, "DECIMAL(19,4)");
            SetTypeMap(DbType.Date, "DATE");
            SetTypeMap(DbType.DateTime, "DATETIME(0)");
            SetTypeMap(DbType.DateTime, "DATETIME($size)", maxSize: 6);
            SetTypeMap(DbType.DateTime2, "DATETIME(6)");
            SetTypeMap(DbType.Decimal, "DECIMAL(19,5)");
            SetTypeMap(DbType.Decimal, "DECIMAL($size,$precision)", DecimalCapacity);
            SetTypeMap(DbType.Double, "DOUBLE");
            SetTypeMap(DbType.Guid, "UUID");
            SetTypeMap(DbType.Int16, "SMALLINT");
            SetTypeMap(DbType.Int32, "INT");
            SetTypeMap(DbType.Int64, "BIGINT");
            SetTypeMap(DbType.Single, "FLOAT");
            SetTypeMap(DbType.StringFixedLength, "CHAR(255) CHARACTER SET utf8mb4");
            SetTypeMap(DbType.StringFixedLength, "CHAR($size) CHARACTER SET utf8mb4", StringCapacity);
            SetTypeMap(DbType.StringFixedLength, "TEXT CHARACTER SET utf8mb4", TextCapacity);
            SetTypeMap(DbType.StringFixedLength, "MEDIUMTEXT CHARACTER SET utf8mb4", MediumTextCapacity);
            SetTypeMap(DbType.StringFixedLength, "LONGTEXT CHARACTER SET utf8mb4", LongTextCapacity);
            SetTypeMap(DbType.String, "VARCHAR(255) CHARACTER SET utf8mb4");
            SetTypeMap(DbType.String, "VARCHAR($size) CHARACTER SET utf8mb4", VarcharCapacity);
            SetTypeMap(DbType.String, "TEXT CHARACTER SET utf8mb4", TextCapacity);
            SetTypeMap(DbType.String, "MEDIUMTEXT CHARACTER SET utf8mb4", MediumTextCapacity);
            SetTypeMap(DbType.String, "LONGTEXT CHARACTER SET utf8mb4", LongTextCapacity);
            SetTypeMap(DbType.Time, "TIME(0)");
            SetTypeMap(DbType.Time, "TIME($size)", maxSize: 6);
            SetTypeMap(DbType.UInt16, "SMALLINT UNSIGNED");
            SetTypeMap(DbType.UInt32, "INT UNSIGNED");
            SetTypeMap(DbType.UInt64, "BIGINT UNSIGNED");
        }

        protected sealed override void SetupTypeMaps()
        {
            SetupMariaDBTypeMaps();
        }
    }
}
