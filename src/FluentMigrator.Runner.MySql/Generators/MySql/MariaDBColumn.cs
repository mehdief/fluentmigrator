﻿#region License
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

using FluentMigrator.Exceptions;
using FluentMigrator.Model;

namespace FluentMigrator.Runner.Generators.MySql
{
    internal class MariaDBColumn : MySql5Column
    {
        public MariaDBColumn(IMariaDBTypeMap typeMap, IQuoter quoter)
            : base(typeMap, quoter)
        {
        }

        protected override string FormatNullable(ColumnDefinition column)
        {
            if (column.Generated != null)
            {
                if (column.IsNullable.HasValue)
                {
                    throw new DatabaseOperationNotSupportedException("MariaDB does not support nullable/non-nullable generated columns");
                }

                return string.Empty;
            }

            return base.FormatNullable(column);
        }
    }
}
