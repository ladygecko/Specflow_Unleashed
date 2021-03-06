﻿using NUnit.Framework;
using OpenQA.Selenium;
using Specflow_Unleashed.Pages;
using System;
using System.Linq;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace Specflow_Unleashed.Steps
{
    [Binding]
    public class SalesSteps

    {
        private AddSalesOrderPage addSalesOrderPage;
        private readonly IWebDriver webDriver;

        private readonly ScenarioContext _scenarioContext;
        public SalesSteps(ScenarioContext scenarioContext) {
            this._scenarioContext = scenarioContext;
            webDriver = scenarioContext.Get<IWebDriver>("currentDriver");
            addSalesOrderPage = new AddSalesOrderPage(_scenarioContext);
            
        }

        [When(@"I click Sales -> Orders -> Add Sales Order")]
        public void WhenIClickSales_Orders_AddSalesOrder()
        {
            DashboardPage dashboardPage = new DashboardPage(_scenarioContext);
            dashboardPage.ClickMenuSales();
            dashboardPage.ClickSubmenuOrders();
            dashboardPage.ClickSubmenuAddSalesOrder();
            
        }
        
        [When(@"I create the sales order with the following details")]
        public void WhenICreateTheSalesOrderWithTheFollowingDetails(Table table)
        {
            dynamic data = table.CreateDynamicInstance();
            addSalesOrderPage.AddOrder(data);
        }
        
        [When(@"I click Add button")]
        public void WhenIClickAddButton() => addSalesOrderPage.ClickAdd();


        [When(@"I click Complete button")]
        public void WhenIClickCompleteButton() => addSalesOrderPage.ClickComplete();
        

        [Then(@"An alert message contains text")]
        public void ThenAnAlertMessageContainsText(Table table)
        {
            dynamic data = table.CreateDynamicInstance();
            Helper helper= new Helper(webDriver);
            helper.checkMessageBoxTextContains(data.Message);
        }

        [Then(@"Stock on hand of product '(.*)' is greater than (.*)")]
        public void ThenStockOnHandOfProductIsGreaterThan(string product, int qty)
        {
            AddSalesOrderPage addSalesOrderPage = new AddSalesOrderPage(_scenarioContext);
            addSalesOrderPage.ClickPrdLink(product);
            webDriver.SwitchTo().Window(webDriver.WindowHandles.Last());
            ProductPage productPage = new ProductPage(_scenarioContext);
            var stockCount = productPage.getNumberOfStockOnHand();

            Assert.IsTrue(stockCount > qty);

        }


    }
}
