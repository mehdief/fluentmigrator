#region License
//
// Copyright (c) 2007-2024, Fluent Migrator Project
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//   http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
#endregion

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;

using FluentMigrator.Builders;
using FluentMigrator.Builders.Create.Table;
using FluentMigrator.Expressions;
using FluentMigrator.Infrastructure;
using FluentMigrator.Model;
using FluentMigrator.SqlServer;

using Moq;

using NUnit.Framework;

using Shouldly;

namespace FluentMigrator.Tests.Unit.Builders.Create
{
    [TestFixture]
    [Category("Builder")]
    [Category("CreateTable")]
    public class CreateTableExpressionBuilderTests
    {
        [Test]
        public void CallingAsAnsiStringSetsColumnDbTypeToAnsiString()
        {
            VerifyColumnDbType(DbType.AnsiString, b => b.AsAnsiString());
        }

        [Test]
        public void CallingAsAnsiStringWithSizeSetsColumnDbTypeToAnsiString()
        {
            VerifyColumnDbType(DbType.AnsiString, b => b.AsAnsiString(255));
        }

        [Test]
        public void CallingAsAnsiStringSetsColumnSizeToSpecifiedValue()
        {
            VerifyColumnSize(255, b => b.AsAnsiString(255));
        }

        [Test]
        public void CallingAsBinarySetsColumnDbTypeToBinary()
        {
            VerifyColumnDbType(DbType.Binary, b => b.AsBinary(255));
        }

        [Test]
        public void CallingAsBinarySetsColumnSizeToSpecifiedValue()
        {
            VerifyColumnSize(255, b => b.AsBinary(255));
        }

        [Test]
        public void CallingAsBooleanSetsColumnDbTypeToBoolean()
        {
            VerifyColumnDbType(DbType.Boolean, b => b.AsBoolean());
        }

        [Test]
        public void CallingAsByteSetsColumnDbTypeToByte()
        {
            VerifyColumnDbType(DbType.Byte, b => b.AsByte());
        }

        [Test]
        public void CallingAsCurrencySetsColumnDbTypeToCurrency()
        {
            VerifyColumnDbType(DbType.Currency, b => b.AsCurrency());
        }

        [Test]
        public void CallingAsDateSetsColumnDbTypeToDate()
        {
            VerifyColumnDbType(DbType.Date, b => b.AsDate());
        }

        [Test]
        public void CallingAsDateTimeSetsColumnDbTypeToDateTime()
        {
            VerifyColumnDbType(DbType.DateTime, b => b.AsDateTime());
        }

        [Test]
        public void CallingAsDateTime2SetsColumnDbTypeToDateTime2()
        {
            VerifyColumnDbType(DbType.DateTime2, b => b.AsDateTime2());
        }

        [Test]
        public void CallingAsDateTimeOffsetSetsColumnDbTypeToDateTimeOffset()
        {
            VerifyColumnDbType(DbType.DateTimeOffset, b => b.AsDateTimeOffset());
        }

        [Test]
        public void CallingAsDecimalSetsColumnDbTypeToDecimal()
        {
            VerifyColumnDbType(DbType.Decimal, b => b.AsDecimal());
        }

        [Test]
        public void CallingAsDecimalWithSizeSetsColumnDbTypeToDecimal()
        {
            VerifyColumnDbType(DbType.Decimal, b => b.AsDecimal(1, 2));
        }

        [Test]
        public void CallingAsDecimalStringSetsColumnSizeToSpecifiedValue()
        {
            VerifyColumnSize(1, b => b.AsDecimal(1, 2));
        }

        [Test]
        public void CallingAsDecimalStringSetsColumnPrecisionToSpecifiedValue()
        {
            VerifyColumnPrecision(2, b => b.AsDecimal(1, 2));
        }

        [Test]
        public void CallingAsDoubleSetsColumnDbTypeToDouble()
        {
            VerifyColumnDbType(DbType.Double, b => b.AsDouble());
        }

        [Test]
        public void CallingAsGuidSetsColumnDbTypeToGuid()
        {
            VerifyColumnDbType(DbType.Guid, b => b.AsGuid());
        }

        [Test]
        public void CallingAsFixedLengthStringSetsColumnDbTypeToStringFixedLength()
        {
            VerifyColumnDbType(DbType.StringFixedLength, e => e.AsFixedLengthString(255));
        }

        [Test]
        public void CallingAsFixedLengthStringSetsColumnSizeToSpecifiedValue()
        {
            VerifyColumnSize(255, b => b.AsFixedLengthString(255));
        }

        [Test]
        public void CallingAsFixedLengthAnsiStringSetsColumnDbTypeToAnsiStringFixedLength()
        {
            VerifyColumnDbType(DbType.AnsiStringFixedLength, b => b.AsFixedLengthAnsiString(255));
        }

        [Test]
        public void CallingAsFixedLengthAnsiStringSetsColumnSizeToSpecifiedValue()
        {
            VerifyColumnSize(255, b => b.AsFixedLengthAnsiString(255));
        }

        [Test]
        public void CallingAsFloatSetsColumnDbTypeToSingle()
        {
            VerifyColumnDbType(DbType.Single, b => b.AsFloat());
        }

        [Test]
        public void CallingAsInt16SetsColumnDbTypeToInt16()
        {
            VerifyColumnDbType(DbType.Int16, b => b.AsInt16());
        }

        [Test]
        public void CallingAsUInt16SetsColumnDbTypeToUInt16()
        {
            VerifyColumnDbType(DbType.UInt16, b => b.AsUInt16());
        }

        [Test]
        public void CallingAsInt32SetsColumnDbTypeToInt32()
        {
            VerifyColumnDbType(DbType.Int32, b => b.AsInt32());
        }

        [Test]
        public void CallingAsUInt32SetsColumnDbTypeToUInt32()
        {
            VerifyColumnDbType(DbType.UInt32, b => b.AsUInt32());
        }

        [Test]
        public void CallingAsInt64SetsColumnDbTypeToInt64()
        {
            VerifyColumnDbType(DbType.Int64, b => b.AsInt64());
        }

        [Test]
        public void CallingAsUInt64SetsColumnDbTypeToUInt64()
        {
            VerifyColumnDbType(DbType.UInt64, b => b.AsUInt64());
        }

        [Test]
        public void CallingAsStringSetsColumnDbTypeToString()
        {
            VerifyColumnDbType(DbType.String, b => b.AsString());
        }

        [Test]
        public void CallingAsStringWithSizeSetsColumnDbTypeToString()
        {
            VerifyColumnDbType(DbType.String, b => b.AsString(255));
        }

        [Test]
        public void CallingAsStringSetsColumnSizeToSpecifiedValue()
        {
            VerifyColumnSize(255, b => b.AsFixedLengthAnsiString(255));
        }

        [Test]
        public void CallingAsTimeSetsColumnDbTypeToTime()
        {
            VerifyColumnDbType(DbType.Time, b => b.AsTime());
        }

        [Test]
        public void CallingAsXmlSetsColumnDbTypeToXml()
        {
            VerifyColumnDbType(DbType.Xml, b => b.AsXml());
        }

        [Test]
        public void CallingAsXmlWithSizeSetsColumnDbTypeToXml()
        {
            VerifyColumnDbType(DbType.Xml, b => b.AsXml(255));
        }

        [Test]
        public void CallingAsXmlSetsColumnSizeToSpecifiedValue()
        {
            VerifyColumnSize(255, b => b.AsXml(255));
        }

        [Test]
        public void CallingAsCustomSetsTypeToNullAndSetsCustomType()
        {
            VerifyColumnProperty(c => c.Type = null, b => b.AsCustom("Test"));
            VerifyColumnProperty(c => c.CustomType = "Test", b => b.AsCustom("Test"));
        }

        [Test]
        public void CallingAsColumnDataTypeWithNonNullCustomTypeSetsTypeToNullAndSetsCustomType()
        {
            VerifyColumnProperty(c => c.Type = null, b => b.AsColumnDataType(new ColumnDataType { CustomType = "Test" }));
            VerifyColumnProperty(c => c.CustomType = "Test", b => b.AsColumnDataType(new ColumnDataType { CustomType = "Test" }));
        }

        [Test]
        public void CallingAsColumnDataTypeSetsOnlyProvidedValue()
        {
            // Only provide Type
            Action<CreateTableExpressionBuilder> provideType = b => b.AsColumnDataType(new ColumnDataType { Type = DbType.Boolean });
            VerifyColumnProperty(c => c.CustomType = null, provideType);
            VerifyColumnProperty(c => c.Type = DbType.Boolean, provideType);
            VerifyColumnProperty(c => c.CollationName = null, provideType);
            VerifyColumnProperty(c => c.Size = null, provideType);
            VerifyColumnProperty(c => c.Precision = null, provideType);
            // Provide type and size
            Action<CreateTableExpressionBuilder> provideTypeSize = b => b.AsColumnDataType(new ColumnDataType { Type = DbType.String, Size = 50 });
            VerifyColumnProperty(c => c.CustomType = null, provideTypeSize);
            VerifyColumnProperty(c => c.Type = DbType.String, provideTypeSize);
            VerifyColumnProperty(c => c.CollationName = null, provideTypeSize);
            VerifyColumnProperty(c => c.Size = 50, provideTypeSize);
            VerifyColumnProperty(c => c.Precision = null, provideTypeSize);
            // Provide type and size with collation
            Action<CreateTableExpressionBuilder> provideTypeSizeCollation = b => b.AsColumnDataType(new ColumnDataType { Type = DbType.AnsiString, Size = 50, CollationName = "test" });
            VerifyColumnProperty(c => c.CustomType = null, provideTypeSizeCollation);
            VerifyColumnProperty(c => c.Type = DbType.AnsiString, provideTypeSizeCollation);
            VerifyColumnProperty(c => c.CollationName = "test", provideTypeSizeCollation);
            VerifyColumnProperty(c => c.Size = 50, provideTypeSizeCollation);
            VerifyColumnProperty(c => c.Precision = null, provideTypeSizeCollation);
            // Provide type and size/precision
            Action<CreateTableExpressionBuilder> provideTypeSizePrecision = b => b.AsColumnDataType(new ColumnDataType { Type = DbType.Decimal, Size = 28, Precision = 10 });
            VerifyColumnProperty(c => c.CustomType = null, provideTypeSizePrecision);
            VerifyColumnProperty(c => c.Type = DbType.Decimal, provideTypeSizePrecision);
            VerifyColumnProperty(c => c.CollationName = null, provideTypeSizePrecision);
            VerifyColumnProperty(c => c.Size = 28, provideTypeSizePrecision);
            VerifyColumnProperty(c => c.Precision = 10, provideTypeSizePrecision);
        }

        [Test]
        public void CallingWithDefaultValueSetsDefaultValue()
        {
            const int value = 42;

            var contextMock = new Mock<IMigrationContext>();

            var columnMock = new Mock<ColumnDefinition>();

            var expressionMock = new Mock<CreateTableExpression>();

            var builder = new CreateTableExpressionBuilder(expressionMock.Object, contextMock.Object);
            builder.CurrentColumn = columnMock.Object;
            builder.WithDefaultValue(42);

            columnMock.VerifySet(c => c.DefaultValue = value);
        }

        [Test]
        public void CallingWithDefaultSetsDefaultValue()
        {
            var contextMock = new Mock<IMigrationContext>();

            var columnMock = new Mock<ColumnDefinition>();

            var expressionMock = new Mock<CreateTableExpression>();

            var builder = new CreateTableExpressionBuilder(expressionMock.Object, contextMock.Object);
            builder.CurrentColumn = columnMock.Object;
            builder.WithDefault(SystemMethods.CurrentDateTime);

            columnMock.VerifySet(c => c.DefaultValue = SystemMethods.CurrentDateTime);
        }

        [Test]
        public void CallingForeignKeySetsIsForeignKeyToTrue()
        {
            VerifyColumnProperty(c => c.IsForeignKey = true, b => b.ForeignKey());
        }

        [Test]
        public void CallingForeignKeySetForeignKey()
        {
            var expressionMock = new Mock<CreateTableExpression>();
            var contextMock = new Mock<IMigrationContext>();
            contextMock.SetupGet(x => x.Expressions).Returns(new List<IMigrationExpression>());
            var builder = new CreateTableExpressionBuilder(expressionMock.Object, contextMock.Object);
            builder.CurrentColumn = new ColumnDefinition();

            var fk = new ForeignKeyDefinition
            {
                Name = "foreignKeyName",
                PrimaryTable = "primaryTableName",
                PrimaryTableSchema = "primaryTableSchema",
                ForeignTable = builder.Expression.TableName,
                ForeignTableSchema = builder.Expression.SchemaName
            };

            builder.ForeignKey(fk.Name, fk.PrimaryTableSchema, fk.PrimaryTable, "primaryColumnName");
            Assert.That(builder.CurrentColumn.IsForeignKey);
            Assert.Multiple(() =>
            {
                Assert.That(fk.Name, Is.EqualTo(builder.CurrentColumn.ForeignKey.Name));
                Assert.That(fk.PrimaryTable, Is.EqualTo(builder.CurrentColumn.ForeignKey.PrimaryTable));
                Assert.That(fk.PrimaryTableSchema, Is.EqualTo(builder.CurrentColumn.ForeignKey.PrimaryTableSchema));
                Assert.That(fk.ForeignTable, Is.EqualTo(builder.CurrentColumn.ForeignKey.ForeignTable));
                Assert.That(fk.ForeignTableSchema, Is.EqualTo(builder.CurrentColumn.ForeignKey.ForeignTableSchema));
            });
        }

        [Test]
        public void CallingIdentitySetsIsIdentityToTrue()
        {
            VerifyColumnProperty(c => c.IsIdentity = true, b => b.Identity());
        }

        [Test]
        public void CallingIdentityWithSeededIdentitySetsAdditionalProperties()
        {
            var contextMock = new Mock<IMigrationContext>();

            var columnMock = new Mock<ColumnDefinition>();

            var expressionMock = new Mock<CreateTableExpression>();
            var builder = new CreateTableExpressionBuilder(expressionMock.Object, contextMock.Object);
            builder.CurrentColumn = columnMock.Object;
            builder.Identity(12, 44);

            columnMock.Object.AdditionalFeatures.ShouldContain(
                new KeyValuePair<string, object>(SqlServerExtensions.IdentitySeed, 12));
            columnMock.Object.AdditionalFeatures.ShouldContain(
                new KeyValuePair<string, object>(SqlServerExtensions.IdentityIncrement, 44));
        }

        [Test]
        public void CallingIdentityWithSeededLongIdentitySetsAdditionalProperties()
        {
            var contextMock = new Mock<IMigrationContext>();

            var columnMock = new Mock<ColumnDefinition>();

            var expressionMock = new Mock<CreateTableExpression>();
            var builder = new CreateTableExpressionBuilder(expressionMock.Object, contextMock.Object);
            builder.CurrentColumn = columnMock.Object;
            builder.Identity(long.MinValue, 44);

            columnMock.Object.AdditionalFeatures.ShouldContain(
                new KeyValuePair<string, object>(SqlServerExtensions.IdentitySeed, long.MinValue));
            columnMock.Object.AdditionalFeatures.ShouldContain(
                new KeyValuePair<string, object>(SqlServerExtensions.IdentityIncrement, 44));
        }

        [Test]
        public void CallingIndexedCallsHelperWithNullIndexName()
        {
            VerifyColumnHelperCall(c => c.Indexed(), h => h.Indexed(null));
        }

        [Test]
        public void CallingIndexedNamedCallsHelperWithName()
        {
            VerifyColumnHelperCall(c => c.Indexed("MyIndexName"), h => h.Indexed("MyIndexName"));
        }

        [Test]
        public void CallingPrimaryKeySetsIsPrimaryKeyToTrue()
        {
            VerifyColumnProperty(c => c.IsPrimaryKey = true, b => b.PrimaryKey());
        }

        [Test]
        public void NullableUsesHelper()
        {
            VerifyColumnHelperCall(c => c.Nullable(), h => h.SetNullable(true));
        }

        [Test]
        public void NotNullableUsesHelper()
        {
            VerifyColumnHelperCall(c => c.NotNullable(), h => h.SetNullable(false));
        }

        [Test]
        public void UniqueUsesHelper()
        {
            VerifyColumnHelperCall(c => c.Unique(), h => h.Unique(null));
        }

        [Test]
        public void NamedUniqueUsesHelper()
        {
            VerifyColumnHelperCall(c => c.Unique("asdf"), h => h.Unique("asdf"));
        }

        [Test]
        public void CallingReferencesAddsNewForeignKeyExpressionToContext()
        {
            var collectionMock = new Mock<ICollection<IMigrationExpression>>();

            var contextMock = new Mock<IMigrationContext>();
            contextMock.Setup(x => x.Expressions).Returns(collectionMock.Object);

            var columnMock = new Mock<ColumnDefinition>();
            columnMock.SetupGet(x => x.Name).Returns("BaconId");

            var expressionMock = new Mock<CreateTableExpression>();
            expressionMock.SetupGet(x => x.TableName).Returns("Bacon");

            var builder = new CreateTableExpressionBuilder(expressionMock.Object, contextMock.Object)
                            {
                                CurrentColumn = columnMock.Object
                            };

            builder.ReferencedBy("fk_foo", "FooTable", "BarColumn");

            collectionMock.Verify(x => x.Add(It.Is<CreateForeignKeyExpression>(
                fk => fk.ForeignKey.Name == "fk_foo" &&
                        fk.ForeignKey.ForeignTable == "FooTable" &&
                        fk.ForeignKey.ForeignColumns.Contains("BarColumn") &&
                        fk.ForeignKey.ForeignColumns.Count == 1 &&
                        fk.ForeignKey.PrimaryTable == "Bacon" &&
                        fk.ForeignKey.PrimaryColumns.Contains("BaconId") &&
                        fk.ForeignKey.PrimaryColumns.Count == 1
                                                )));

            contextMock.VerifyGet(x => x.Expressions);
        }

        [Test]
        public void CallingReferencedByAddsNewForeignKeyExpressionToContext()
        {
            var collectionMock = new Mock<ICollection<IMigrationExpression>>();

            var contextMock = new Mock<IMigrationContext>();
            contextMock.Setup(x => x.Expressions).Returns(collectionMock.Object);

            var columnMock = new Mock<ColumnDefinition>();
            columnMock.SetupGet(x => x.Name).Returns("BaconId");

            var expressionMock = new Mock<CreateTableExpression>();
            expressionMock.SetupGet(x => x.TableName).Returns("Bacon");

            var builder = new CreateTableExpressionBuilder(expressionMock.Object, contextMock.Object)
                            {
                                CurrentColumn = columnMock.Object
                            };

            builder.ReferencedBy("fk_foo", "FooTable", "BarColumn");

            collectionMock.Verify(x => x.Add(It.Is<CreateForeignKeyExpression>(
                fk => fk.ForeignKey.Name == "fk_foo" &&
                        fk.ForeignKey.ForeignTable == "FooTable" &&
                        fk.ForeignKey.ForeignColumns.Contains("BarColumn") &&
                        fk.ForeignKey.ForeignColumns.Count == 1 &&
                        fk.ForeignKey.PrimaryTable == "Bacon" &&
                        fk.ForeignKey.PrimaryColumns.Contains("BaconId") &&
                        fk.ForeignKey.PrimaryColumns.Count == 1
                                                )));

            contextMock.VerifyGet(x => x.Expressions);
        }

        [Test]
        public void CallingForeignKeyAddsNewForeignKeyExpressionToContext()
        {
            var collectionMock = new Mock<ICollection<IMigrationExpression>>();

            var contextMock = new Mock<IMigrationContext>();
            contextMock.Setup(x => x.Expressions).Returns(collectionMock.Object);

            var columnMock = new Mock<ColumnDefinition>();
            columnMock.SetupGet(x => x.Name).Returns("BaconId");

            var expressionMock = new Mock<CreateTableExpression>();
            expressionMock.SetupGet(x => x.TableName).Returns("Bacon");

            var builder = new CreateTableExpressionBuilder(expressionMock.Object, contextMock.Object)
                            {
                                CurrentColumn = columnMock.Object
                            };

            builder.ForeignKey("fk_foo", "FooTable", "BarColumn");

            collectionMock.Verify(x => x.Add(It.Is<CreateForeignKeyExpression>(
                fk => fk.ForeignKey.Name == "fk_foo" &&
                        fk.ForeignKey.PrimaryTable == "FooTable" &&
                        fk.ForeignKey.PrimaryColumns.Contains("BarColumn") &&
                        fk.ForeignKey.PrimaryColumns.Count == 1 &&
                        fk.ForeignKey.ForeignTable == "Bacon" &&
                        fk.ForeignKey.ForeignColumns.Contains("BaconId") &&
                        fk.ForeignKey.ForeignColumns.Count == 1
                                                )));

            contextMock.VerifyGet(x => x.Expressions);
        }

        [TestCase(Rule.Cascade), TestCase(Rule.SetDefault), TestCase(Rule.SetNull), TestCase(Rule.None)]
        public void CallingOnUpdateSetsOnUpdateOnForeignKeyExpression(Rule rule)
        {
            var builder = new CreateTableExpressionBuilder(null, null) { CurrentForeignKey = new ForeignKeyDefinition() };
            builder.OnUpdate(rule);
            Assert.Multiple(() =>
            {
                Assert.That(builder.CurrentForeignKey.OnUpdate, Is.EqualTo(rule));
                Assert.That(builder.CurrentForeignKey.OnDelete, Is.EqualTo(Rule.None));
            });
        }

        [TestCase(Rule.Cascade), TestCase(Rule.SetDefault), TestCase(Rule.SetNull), TestCase(Rule.None)]
        public void CallingOnDeleteSetsOnDeleteOnForeignKeyExpression(Rule rule)
        {
            var builder = new CreateTableExpressionBuilder(null, null) { CurrentForeignKey = new ForeignKeyDefinition() };
            builder.OnDelete(rule);
            Assert.Multiple(() =>
            {
                Assert.That(builder.CurrentForeignKey.OnUpdate, Is.EqualTo(Rule.None));
                Assert.That(builder.CurrentForeignKey.OnDelete, Is.EqualTo(rule));
            });
        }

        [TestCase(Rule.Cascade), TestCase(Rule.SetDefault), TestCase(Rule.SetNull), TestCase(Rule.None)]
        public void CallingOnDeleteOrUpdateSetsOnUpdateAndOnDeleteOnForeignKeyExpression(Rule rule)
        {
            var builder = new CreateTableExpressionBuilder(null, null) { CurrentForeignKey = new ForeignKeyDefinition() };
            builder.OnDeleteOrUpdate(rule);
            Assert.Multiple(() =>
            {
                Assert.That(builder.CurrentForeignKey.OnUpdate, Is.EqualTo(rule));
                Assert.That(builder.CurrentForeignKey.OnDelete, Is.EqualTo(rule));
            });
        }

        [Test]
        public void CallingWithColumnAddsNewColumnToExpression()
        {
            const string name = "BaconId";

            var collectionMock = new Mock<IList<ColumnDefinition>>();

            var expressionMock = new Mock<CreateTableExpression>();
            expressionMock.SetupGet(e => e.Columns).Returns(collectionMock.Object);

            var contextMock = new Mock<IMigrationContext>();

            var builder = new CreateTableExpressionBuilder(expressionMock.Object, contextMock.Object);
            builder.WithColumn(name);

            collectionMock.Verify(x => x.Add(It.Is<ColumnDefinition>(c => c.Name.Equals(name))));
            expressionMock.VerifyGet(e => e.Columns);
        }

        [Test]
        public void ColumnHelperSetOnCreation()
        {
            var expressionMock = new Mock<CreateTableExpression>();
            var contextMock = new Mock<IMigrationContext>();

            var builder = new CreateTableExpressionBuilder(expressionMock.Object, contextMock.Object);

            Assert.That(builder.ColumnHelper, Is.Not.Null);
        }

        [Test]
        public void ColumnExpressionBuilderUsesExpressionSchemaAndTableName()
        {
            var expressionMock = new Mock<CreateTableExpression>();
            var contextMock = new Mock<IMigrationContext>();
            expressionMock.SetupGet(n => n.SchemaName).Returns("Fred");
            expressionMock.SetupGet(n => n.TableName).Returns("Flinstone");

            var builder = new CreateTableExpressionBuilder(expressionMock.Object, contextMock.Object);
            var builderAsInterface = (IColumnExpressionBuilder)builder;

            Assert.Multiple(() =>
            {
                Assert.That(builderAsInterface.SchemaName, Is.EqualTo("Fred"));
                Assert.That(builderAsInterface.TableName, Is.EqualTo("Flinstone"));
            });
        }

        [Test]
        public void ColumnExpressionBuilderUsesCurrentColumn()
        {
            var expressionMock = new Mock<CreateTableExpression>();
            var contextMock = new Mock<IMigrationContext>();

            var builder = new CreateTableExpressionBuilder(expressionMock.Object, contextMock.Object);

            var curColumn = new Mock<ColumnDefinition>().Object;
            builder.CurrentColumn = curColumn;

            var builderAsInterface = (IColumnExpressionBuilder)builder;

            Assert.That(builderAsInterface.Column, Is.SameAs(curColumn));
        }

        private void VerifyColumnHelperCall(Action<CreateTableExpressionBuilder> callToTest, System.Linq.Expressions.Expression<Action<ColumnExpressionBuilderHelper>> expectedHelperAction)
        {
            var expressionMock = new Mock<CreateTableExpression>();
            var contextMock = new Mock<IMigrationContext>();
            var helperMock = new Mock<ColumnExpressionBuilderHelper>();

            var builder = new CreateTableExpressionBuilder(expressionMock.Object, contextMock.Object);
            builder.ColumnHelper = helperMock.Object;

            callToTest(builder);

            helperMock.Verify(expectedHelperAction);
        }

        private void VerifyColumnProperty(Action<ColumnDefinition> columnExpression, Action<CreateTableExpressionBuilder> callToTest)
        {
            var columnMock = new Mock<ColumnDefinition>();

            var expressionMock = new Mock<CreateTableExpression>();

            var contextMock = new Mock<IMigrationContext>();
            contextMock.SetupGet(mc => mc.Expressions).Returns(new Collection<IMigrationExpression>());

            var builder = new CreateTableExpressionBuilder(expressionMock.Object, contextMock.Object);
            builder.CurrentColumn = columnMock.Object;

            callToTest(builder);

            columnMock.VerifySet(columnExpression);
        }

        private void VerifyColumnDbType(DbType expected, Action<CreateTableExpressionBuilder> callToTest)
        {
            var columnMock = new Mock<ColumnDefinition>();

            var expressionMock = new Mock<CreateTableExpression>();

            var contextMock = new Mock<IMigrationContext>();

            var builder = new CreateTableExpressionBuilder(expressionMock.Object, contextMock.Object);
            builder.CurrentColumn = columnMock.Object;

            callToTest(builder);

            columnMock.VerifySet(c => c.Type = expected);
        }

        private void VerifyColumnSize(int expected, Action<CreateTableExpressionBuilder> callToTest)
        {
            var columnMock = new Mock<ColumnDefinition>();

            var expressionMock = new Mock<CreateTableExpression>();

            var contextMock = new Mock<IMigrationContext>();

            var builder = new CreateTableExpressionBuilder(expressionMock.Object, contextMock.Object);
            builder.CurrentColumn = columnMock.Object;

            callToTest(builder);

            columnMock.VerifySet(c => c.Size = expected);
        }

        private void VerifyColumnPrecision(int expected, Action<CreateTableExpressionBuilder> callToTest)
        {
            var columnMock = new Mock<ColumnDefinition>();

            var expressionMock = new Mock<CreateTableExpression>();

            var contextMock = new Mock<IMigrationContext>();

            var builder = new CreateTableExpressionBuilder(expressionMock.Object, contextMock.Object);
            builder.CurrentColumn = columnMock.Object;

            callToTest(builder);

            columnMock.VerifySet(c => c.Precision = expected);
        }
    }
}
