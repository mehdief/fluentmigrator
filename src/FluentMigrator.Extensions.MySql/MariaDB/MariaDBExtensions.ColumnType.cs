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

using FluentMigrator.Builders;
using FluentMigrator.Infrastructure;

namespace FluentMigrator.MySql
{
    public static partial class MariaDBExtensions
    {
        public static TNext AsSByte<TNext>(this IColumnTypeSyntax<TNext> column)
            where TNext : IFluentSyntax
        {
            return column.AsCustom("TINYINT SIGNED");
        }

        public static TNext AsText<TNext>(this IColumnTypeSyntax<TNext> column)
            where TNext : IFluentSyntax
        {
            return column.AsString(size: 65535);
        }

        public static TNext AsText<TNext>(this IColumnTypeSyntax<TNext> column, string collationName)
            where TNext : IFluentSyntax
        {
            return column.AsString(size: 65535, collationName: collationName);
        }

        public static TNext AsText<TNext>(this IColumnTypeSyntax<TNext> column, bool caseSensitive)
            where TNext : IFluentSyntax
        {
            return column.AsString(size: 65535, caseSensitive: caseSensitive);
        }

        public static TNext AsMediumText<TNext>(this IColumnTypeSyntax<TNext> column)
            where TNext : IFluentSyntax
        {
            return column.AsString(size: 16777215);
        }

        public static TNext AsMediumText<TNext>(this IColumnTypeSyntax<TNext> column, string collationName)
            where TNext : IFluentSyntax
        {
            return column.AsString(size: 16777215, collationName: collationName);
        }

        public static TNext AsMediumText<TNext>(this IColumnTypeSyntax<TNext> column, bool caseSensitive)
            where TNext : IFluentSyntax
        {
            return column.AsString(size: 16777215, caseSensitive: caseSensitive);
        }

        public static TNext AsLongText<TNext>(this IColumnTypeSyntax<TNext> column)
            where TNext : IFluentSyntax
        {
            return column.AsString(size: int.MaxValue);
        }

        public static TNext AsLongText<TNext>(this IColumnTypeSyntax<TNext> column, string collationName)
            where TNext : IFluentSyntax
        {
            return column.AsString(size: int.MaxValue, collationName: collationName);
        }

        public static TNext AsLongText<TNext>(this IColumnTypeSyntax<TNext> column, bool caseSensitive)
            where TNext : IFluentSyntax
        {
            return column.AsString(size: int.MaxValue, caseSensitive: caseSensitive);
        }

        public static TNext AsBlob<TNext>(this IColumnTypeSyntax<TNext> column)
            where TNext : IFluentSyntax
        {
            return column.AsBinary(size: 65535);
        }

        public static TNext AsMediumBlob<TNext>(this IColumnTypeSyntax<TNext> column)
            where TNext : IFluentSyntax
        {
            return column.AsBinary(size: 16777215);
        }

        public static TNext AsLongBlob<TNext>(this IColumnTypeSyntax<TNext> column)
            where TNext : IFluentSyntax
        {
            return column.AsBinary(size: int.MaxValue);
        }

        public static TNext AsJson<TNext>(this IColumnTypeSyntax<TNext> column)
            where TNext : IFluentSyntax
        {
            return column.AsCustom("JSON");
        }
    }
}
