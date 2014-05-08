﻿// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by SpecFlow (http://www.specflow.org/).
//      SpecFlow Version:1.9.0.77
//      SpecFlow Generator Version:1.9.0.0
//      Runtime Version:4.0.30319.34011
// 
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------
#region Designer generated code
#pragma warning disable
namespace TomorrowComesToday.Tests.SpecflowTests.Features
{
    using TechTalk.SpecFlow;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "1.9.0.77")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [NUnit.Framework.TestFixtureAttribute()]
    [NUnit.Framework.DescriptionAttribute("GameRoundTests")]
    public partial class GameRoundTestsFeature
    {
        
        private static TechTalk.SpecFlow.ITestRunner testRunner;
        
#line 1 "GameRoundTests.feature"
#line hidden
        
        [NUnit.Framework.TestFixtureSetUpAttribute()]
        public virtual void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "GameRoundTests", "While the game is operational\r\nI want to ensure the rounds function as expected\r\n" +
                    "To avoid the game being crap", ProgrammingLanguage.CSharp, ((string[])(null)));
            testRunner.OnFeatureStart(featureInfo);
        }
        
        [NUnit.Framework.TestFixtureTearDownAttribute()]
        public virtual void FeatureTearDown()
        {
            testRunner.OnFeatureEnd();
            testRunner = null;
        }
        
        [NUnit.Framework.SetUpAttribute()]
        public virtual void TestInitialize()
        {
        }
        
        [NUnit.Framework.TearDownAttribute()]
        public virtual void ScenarioTearDown()
        {
            testRunner.OnScenarioEnd();
        }
        
        public virtual void ScenarioSetup(TechTalk.SpecFlow.ScenarioInfo scenarioInfo)
        {
            testRunner.OnScenarioStart(scenarioInfo);
        }
        
        public virtual void ScenarioCleanup()
        {
            testRunner.CollectScenarioErrors();
        }
        
        public virtual void FeatureBackground()
        {
#line 6
#line 7
 testRunner.Given("I have an initalised back end", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
            TechTalk.SpecFlow.Table table1 = new TechTalk.SpecFlow.Table(new string[] {
                        "Name"});
            table1.AddRow(new string[] {
                        "James"});
            table1.AddRow(new string[] {
                        "Jean Luc"});
            table1.AddRow(new string[] {
                        "Benjamin"});
            table1.AddRow(new string[] {
                        "Kathryn"});
#line 8
 testRunner.And("I have the following players:", ((string)(null)), table1, "And ");
#line hidden
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("When a game starts users are handed their cards")]
        public virtual void WhenAGameStartsUsersAreHandedTheirCards()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("When a game starts users are handed their cards", ((string[])(null)));
#line 17
this.ScenarioSetup(scenarioInfo);
#line 6
this.FeatureBackground();
#line hidden
            TechTalk.SpecFlow.Table table2 = new TechTalk.SpecFlow.Table(new string[] {
                        "Name"});
            table2.AddRow(new string[] {
                        "Jean Luc"});
            table2.AddRow(new string[] {
                        "Benjamin"});
#line 18
 testRunner.Given("I have a started game with the id \'F4909379-AF76-418E-873D-E575A8BA3233\' containi" +
                    "ng following players:", ((string)(null)), table2, "Given ");
#line 22
 testRunner.And("the game \'F4909379-AF76-418E-873D-E575A8BA3233\' has a white deck of \'50\' cards", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 23
 testRunner.Then("I see the game \'F4909379-AF76-418E-873D-E575A8BA3233\' is in state \'Active\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            TechTalk.SpecFlow.Table table3 = new TechTalk.SpecFlow.Table(new string[] {
                        "Name",
                        "Points",
                        "Cards in hand"});
            table3.AddRow(new string[] {
                        "Jean Luc",
                        "0",
                        "10"});
            table3.AddRow(new string[] {
                        "Benjamin",
                        "0",
                        "10"});
#line 24
 testRunner.And("I see the game \'F4909379-AF76-418E-873D-E575A8BA3233\' players are in the followin" +
                    "g state:", ((string)(null)), table3, "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Cards in deck when not enough left are properly shared among players")]
        public virtual void CardsInDeckWhenNotEnoughLeftAreProperlySharedAmongPlayers()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Cards in deck when not enough left are properly shared among players", ((string[])(null)));
#line 30
this.ScenarioSetup(scenarioInfo);
#line 6
this.FeatureBackground();
#line hidden
            TechTalk.SpecFlow.Table table4 = new TechTalk.SpecFlow.Table(new string[] {
                        "Name"});
            table4.AddRow(new string[] {
                        "Jean Luc"});
            table4.AddRow(new string[] {
                        "Benjamin"});
#line 31
 testRunner.Given("I have a started game with the id \'F4909379-AF76-418E-873D-E575A8BA3233\' containi" +
                    "ng following players:", ((string)(null)), table4, "Given ");
#line 35
 testRunner.And("the game \'F4909379-AF76-418E-873D-E575A8BA3233\' has a white deck of \'11\' cards", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 36
 testRunner.Then("I see the game \'F4909379-AF76-418E-873D-E575A8BA3233\' is in state \'Active\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            TechTalk.SpecFlow.Table table5 = new TechTalk.SpecFlow.Table(new string[] {
                        "Name",
                        "Points",
                        "Cards in hand"});
            table5.AddRow(new string[] {
                        "Jean Luc",
                        "0",
                        "6"});
            table5.AddRow(new string[] {
                        "Benjamin",
                        "0",
                        "5"});
#line 37
 testRunner.And("I see the game \'F4909379-AF76-418E-873D-E575A8BA3233\' players are in the followin" +
                    "g state:", ((string)(null)), table5, "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("A card tsar is assigned and a black card is played as the game starts")]
        public virtual void ACardTsarIsAssignedAndABlackCardIsPlayedAsTheGameStarts()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("A card tsar is assigned and a black card is played as the game starts", ((string[])(null)));
#line 44
this.ScenarioSetup(scenarioInfo);
#line 6
this.FeatureBackground();
#line hidden
            TechTalk.SpecFlow.Table table6 = new TechTalk.SpecFlow.Table(new string[] {
                        "Name"});
            table6.AddRow(new string[] {
                        "Jean Luc"});
            table6.AddRow(new string[] {
                        "Benjamin"});
#line 45
 testRunner.Given("I have a started game with the id \'F4909379-AF76-418E-873D-E575A8BA3233\' containi" +
                    "ng following players:", ((string)(null)), table6, "Given ");
#line 49
 testRunner.And("the game \'F4909379-AF76-418E-873D-E575A8BA3233\' has a white deck of \'50\' cards", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 50
 testRunner.Then("I see the game \'F4909379-AF76-418E-873D-E575A8BA3233\' is in state \'Active\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 51
 testRunner.And("I see the game \'F4909379-AF76-418E-873D-E575A8BA3233\' has an active black card", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 52
 testRunner.And("I see the game \'F4909379-AF76-418E-873D-E575A8BA3233\' has an active player", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
    }
}
#pragma warning restore
#endregion
