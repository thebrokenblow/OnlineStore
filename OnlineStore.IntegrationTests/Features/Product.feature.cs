﻿// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by Reqnroll (https://www.reqnroll.net/).
//      Reqnroll Version:2.0.0.0
//      Reqnroll Generator Version:2.0.0.0
// 
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------
#region Designer generated code
#pragma warning disable
namespace OnlineStore.IntegrationTests.Features
{
    using Reqnroll;
    using System;
    using System.Linq;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Reqnroll", "2.0.0.0")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public partial class ManagementAListOfProductFeature : object, Xunit.IClassFixture<ManagementAListOfProductFeature.FixtureData>, Xunit.IAsyncLifetime
    {
        
        private global::Reqnroll.ITestRunner testRunner;
        
        private static string[] featureTags = ((string[])(null));
        
        private static global::Reqnroll.FeatureInfo featureInfo = new global::Reqnroll.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "Features", "management a list of product", null, global::Reqnroll.ProgrammingLanguage.CSharp, featureTags);
        
        private Xunit.Abstractions.ITestOutputHelper _testOutputHelper;
        
#line 1 "Product.feature"
#line hidden
        
        public ManagementAListOfProductFeature(ManagementAListOfProductFeature.FixtureData fixtureData, Xunit.Abstractions.ITestOutputHelper testOutputHelper)
        {
            this._testOutputHelper = testOutputHelper;
        }
        
        public static async System.Threading.Tasks.Task FeatureSetupAsync()
        {
        }
        
        public static async System.Threading.Tasks.Task FeatureTearDownAsync()
        {
        }
        
        public async System.Threading.Tasks.Task TestInitializeAsync()
        {
            testRunner = global::Reqnroll.TestRunnerManager.GetTestRunnerForAssembly(featureHint: featureInfo);
            if (((testRunner.FeatureContext != null) 
                        && (testRunner.FeatureContext.FeatureInfo.Equals(featureInfo) == false)))
            {
                await testRunner.OnFeatureEndAsync();
            }
            if ((testRunner.FeatureContext == null))
            {
                await testRunner.OnFeatureStartAsync(featureInfo);
            }
        }
        
        public async System.Threading.Tasks.Task TestTearDownAsync()
        {
            await testRunner.OnScenarioEndAsync();
            global::Reqnroll.TestRunnerManager.ReleaseTestRunner(testRunner);
        }
        
        public void ScenarioInitialize(global::Reqnroll.ScenarioInfo scenarioInfo)
        {
            testRunner.OnScenarioInitialize(scenarioInfo);
            testRunner.ScenarioContext.ScenarioContainer.RegisterInstanceAs<Xunit.Abstractions.ITestOutputHelper>(_testOutputHelper);
        }
        
        public async System.Threading.Tasks.Task ScenarioStartAsync()
        {
            await testRunner.OnScenarioStartAsync();
        }
        
        public async System.Threading.Tasks.Task ScenarioCleanupAsync()
        {
            await testRunner.CollectScenarioErrorsAsync();
        }
        
        public virtual async System.Threading.Tasks.Task FeatureBackgroundAsync()
        {
#line 3
    #line hidden
        }
        
        async System.Threading.Tasks.Task Xunit.IAsyncLifetime.InitializeAsync()
        {
            await this.TestInitializeAsync();
        }
        
        async System.Threading.Tasks.Task Xunit.IAsyncLifetime.DisposeAsync()
        {
            await this.TestTearDownAsync();
        }
        
        [Xunit.SkippableFactAttribute(DisplayName="We can create several product categories")]
        [Xunit.TraitAttribute("FeatureTitle", "management a list of product")]
        [Xunit.TraitAttribute("Description", "We can create several product categories")]
        public async System.Threading.Tasks.Task WeCanCreateSeveralProductCategories()
        {
            string[] tagsOfScenario = ((string[])(null));
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            global::Reqnroll.ScenarioInfo scenarioInfo = new global::Reqnroll.ScenarioInfo("We can create several product categories", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 5
    this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((global::Reqnroll.TagHelper.ContainsIgnoreTag(scenarioInfo.CombinedTags) || global::Reqnroll.TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                await this.ScenarioStartAsync();
#line 3
    await this.FeatureBackgroundAsync();
#line hidden
                global::Reqnroll.Table table1 = new global::Reqnroll.Table(new string[] {
                            "Name",
                            "Description"});
                table1.AddRow(new string[] {
                            "Electronics",
                            "Devices and gadgets for everyday use"});
                table1.AddRow(new string[] {
                            "Home Appliances",
                            "Appliances for kitchen and home use"});
#line 7
        await testRunner.GivenAsync("we want to add several products, but to add products we have to add categories:", ((string)(null)), table1, "Given ");
#line hidden
                global::Reqnroll.Table table2 = new global::Reqnroll.Table(new string[] {
                            "Name",
                            "Description",
                            "Price",
                            "CategoryName"});
                table2.AddRow(new string[] {
                            "Wireless Mouse",
                            "Ergonomic wireless mouse with 3 buttons",
                            "24.99",
                            "Electronics"});
                table2.AddRow(new string[] {
                            "Portable Speaker",
                            "Compact Bluetooth speaker with 10-hour battery life",
                            "49.99",
                            "Electronics"});
                table2.AddRow(new string[] {
                            "Coffee Maker",
                            "Automatic drip coffee maker with 12-cup capacity",
                            "59.99",
                            "Home Appliances"});
#line 12
        await testRunner.WhenAsync("we add several products:", ((string)(null)), table2, "When ");
#line hidden
                global::Reqnroll.Table table3 = new global::Reqnroll.Table(new string[] {
                            "Name",
                            "Price"});
                table3.AddRow(new string[] {
                            "Wireless Mouse",
                            "24.99"});
                table3.AddRow(new string[] {
                            "Portable Speaker",
                            "49.99"});
                table3.AddRow(new string[] {
                            "Coffee Maker",
                            "59.99"});
#line 18
        await testRunner.ThenAsync("we can get all records of products:", ((string)(null)), table3, "Then ");
#line hidden
            }
            await this.ScenarioCleanupAsync();
        }
        
        [Xunit.SkippableFactAttribute(DisplayName="We can create several product and update one")]
        [Xunit.TraitAttribute("FeatureTitle", "management a list of product")]
        [Xunit.TraitAttribute("Description", "We can create several product and update one")]
        public async System.Threading.Tasks.Task WeCanCreateSeveralProductAndUpdateOne()
        {
            string[] tagsOfScenario = ((string[])(null));
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            global::Reqnroll.ScenarioInfo scenarioInfo = new global::Reqnroll.ScenarioInfo("We can create several product and update one", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 24
    this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((global::Reqnroll.TagHelper.ContainsIgnoreTag(scenarioInfo.CombinedTags) || global::Reqnroll.TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                await this.ScenarioStartAsync();
#line 3
    await this.FeatureBackgroundAsync();
#line hidden
                global::Reqnroll.Table table4 = new global::Reqnroll.Table(new string[] {
                            "Name",
                            "Description"});
                table4.AddRow(new string[] {
                            "Electronics",
                            "Devices and gadgets for everyday use"});
                table4.AddRow(new string[] {
                            "Home Appliances",
                            "Appliances for kitchen and home use"});
#line 26
     await testRunner.GivenAsync("we want to add several products and then update one, but to add products we have " +
                        "to add categories:", ((string)(null)), table4, "Given ");
#line hidden
                global::Reqnroll.Table table5 = new global::Reqnroll.Table(new string[] {
                            "Name",
                            "Description",
                            "Price",
                            "CategoryName"});
                table5.AddRow(new string[] {
                            "Wireless Mouse",
                            "Ergonomic wireless mouse with 3 buttons",
                            "24.99",
                            "Electronics"});
                table5.AddRow(new string[] {
                            "Portable Speaker",
                            "Compact Bluetooth speaker with 10-hour battery life",
                            "49.99",
                            "Electronics"});
                table5.AddRow(new string[] {
                            "Coffee Maker",
                            "Automatic drip coffee maker with 12-cup capacity",
                            "59.99",
                            "Home Appliances"});
#line 31
     await testRunner.WhenAsync("we add several products:", ((string)(null)), table5, "When ");
#line hidden
                global::Reqnroll.Table table6 = new global::Reqnroll.Table(new string[] {
                            "Name",
                            "Description",
                            "Price",
                            "CategoryName"});
                table6.AddRow(new string[] {
                            "Smartwatch",
                            "Waterproof smartwatch with heart rate monitor",
                            "149.99",
                            "Electronics"});
#line 37
     await testRunner.AndAsync("we update first product:", ((string)(null)), table6, "And ");
#line hidden
                global::Reqnroll.Table table7 = new global::Reqnroll.Table(new string[] {
                            "Name",
                            "Price"});
                table7.AddRow(new string[] {
                            "Portable Speaker",
                            "49.99"});
                table7.AddRow(new string[] {
                            "Coffee Maker",
                            "59.99"});
                table7.AddRow(new string[] {
                            "Smartwatch",
                            "149.99"});
#line 41
     await testRunner.ThenAsync("we get all products after the update:", ((string)(null)), table7, "Then ");
#line hidden
#line 47
    await testRunner.AndAsync("we get details updated product", ((string)(null)), ((global::Reqnroll.Table)(null)), "And ");
#line hidden
            }
            await this.ScenarioCleanupAsync();
        }
        
        [System.CodeDom.Compiler.GeneratedCodeAttribute("Reqnroll", "2.0.0.0")]
        [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
        public class FixtureData : object, Xunit.IAsyncLifetime
        {
            
            async System.Threading.Tasks.Task Xunit.IAsyncLifetime.InitializeAsync()
            {
                await ManagementAListOfProductFeature.FeatureSetupAsync();
            }
            
            async System.Threading.Tasks.Task Xunit.IAsyncLifetime.DisposeAsync()
            {
                await ManagementAListOfProductFeature.FeatureTearDownAsync();
            }
        }
    }
}
#pragma warning restore
#endregion
