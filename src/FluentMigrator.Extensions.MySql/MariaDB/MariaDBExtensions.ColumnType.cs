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
        public const string CaseSensitiveCollationName = "utf8mb4_bin";
        public const string AnsiCaseSensitiveCollationName = "utf8mb3_bin";
        public const int TextCapacity = 65535;
        public const int MediumTextCapacity = 16777215;

        public static TNext AsSByte<TNext>(this IColumnTypeSyntax<TNext> column)
            where TNext : IFluentSyntax
        {
            return column.AsCustom("TINYINT(1) SIGNED");
        }

        public static TNext AsAnsiString<TNext>(this IColumnTypeSyntax<TNext> column, bool caseSensitive)
            where TNext : IFluentSyntax
        {
            return caseSensitive
                ? column.AsAnsiString(AnsiCaseSensitiveCollationName)
                : column.AsAnsiString();
        }

        public static TNext AsAnsiString<TNext>(this IColumnTypeSyntax<TNext> column, int size, bool caseSensitive)
            where TNext : IFluentSyntax
        {
            return caseSensitive
                ? column.AsAnsiString(size, AnsiCaseSensitiveCollationName)
                : column.AsAnsiString(size);
        }

        public static TNext AsFixedLengthString<TNext>(this IColumnTypeSyntax<TNext> column, int size, bool caseSensitive)
            where TNext : IFluentSyntax
        {
            return caseSensitive
                ? column.AsFixedLengthString(size, CaseSensitiveCollationName)
                : column.AsFixedLengthString(size);
        }

        public static TNext AsFixedLengthAnsiString<TNext>(this IColumnTypeSyntax<TNext> column, int size, bool caseSensitive)
            where TNext : IFluentSyntax
        {
            return caseSensitive
                ? column.AsFixedLengthAnsiString(size, AnsiCaseSensitiveCollationName)
                : column.AsFixedLengthAnsiString(size);
        }

        public static TNext AsString<TNext>(this IColumnTypeSyntax<TNext> column, bool caseSensitive)
            where TNext : IFluentSyntax
        {
            return caseSensitive
                ? column.AsString(CaseSensitiveCollationName)
                : column.AsString();
        }

        public static TNext AsString<TNext>(this IColumnTypeSyntax<TNext> column, int size, bool caseSensitive)
            where TNext : IFluentSyntax
        {
            return caseSensitive
                ? column.AsString(size, CaseSensitiveCollationName)
                : column.AsString(size);
        }

        public static TNext AsText<TNext>(this IColumnTypeSyntax<TNext> column)
            where TNext : IFluentSyntax
        {
            return column.AsString(TextCapacity);
        }

        public static TNext AsText<TNext>(this IColumnTypeSyntax<TNext> column, string collationName)
            where TNext : IFluentSyntax
        {
            return column.AsString(TextCapacity, collationName);
        }

        public static TNext AsText<TNext>(this IColumnTypeSyntax<TNext> column, bool caseSensitive)
            where TNext : IFluentSyntax
        {
            return column.AsString(TextCapacity, caseSensitive);
        }

        public static TNext AsMediumText<TNext>(this IColumnTypeSyntax<TNext> column)
            where TNext : IFluentSyntax
        {
            return column.AsString(MediumTextCapacity);
        }

        public static TNext AsMediumText<TNext>(this IColumnTypeSyntax<TNext> column, string collationName)
            where TNext : IFluentSyntax
        {
            return column.AsString(MediumTextCapacity, collationName);
        }

        public static TNext AsMediumText<TNext>(this IColumnTypeSyntax<TNext> column, bool caseSensitive)
            where TNext : IFluentSyntax
        {
            return column.AsString(MediumTextCapacity, caseSensitive);
        }

        public static TNext AsLongText<TNext>(this IColumnTypeSyntax<TNext> column)
            where TNext : IFluentSyntax
        {
            return column.AsString(int.MaxValue);
        }

        public static TNext AsLongText<TNext>(this IColumnTypeSyntax<TNext> column, string collationName)
            where TNext : IFluentSyntax
        {
            return column.AsString(int.MaxValue, collationName);
        }

        public static TNext AsLongText<TNext>(this IColumnTypeSyntax<TNext> column, bool caseSensitive)
            where TNext : IFluentSyntax
        {
            return column.AsString(int.MaxValue, caseSensitive);
        }

        public static TNext AsBlob<TNext>(this IColumnTypeSyntax<TNext> column)
            where TNext : IFluentSyntax
        {
            return column.AsBinary(TextCapacity);
        }

        public static TNext AsMediumBlob<TNext>(this IColumnTypeSyntax<TNext> column)
            where TNext : IFluentSyntax
        {
            return column.AsBinary(MediumTextCapacity);
        }

        public static TNext AsLongBlob<TNext>(this IColumnTypeSyntax<TNext> column)
            where TNext : IFluentSyntax
        {
            return column.AsBinary(int.MaxValue);
        }

        public static TNext AsJson<TNext>(this IColumnTypeSyntax<TNext> column)
            where TNext : IFluentSyntax
        {
            return column.AsCustom("JSON");
        }
    }
}
