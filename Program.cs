using HtmlAgilityPack;
using CsvHelper;
using System.Globalization;
using OpenQA.Selenium; 
using OpenQA.Selenium.Chrome;


var products = new List<Product>(); 
 
			// to open Chrome in headless mode 
			var chromeOptions = new ChromeOptions(); 
			chromeOptions.AddArguments("headless"); 
 
			// starting a Selenium instance 
			using (var driver = new ChromeDriver(chromeOptions)) 
			{ 
				// navigating to the target page in the browser 
				driver.Navigate().GoToUrl("https://www.scrapingcourse.com/ecommerce/"); 
 
				// getting the HTML product elements 
				var productHTMLElements = driver.FindElements(By.CssSelector("li.product")); 
				// iterating over them to scrape the data of interest 
				foreach (var productHTMLElement in productHTMLElements) 
				{ 
					// scraping logic 
					var url = productHTMLElement.FindElement(By.CssSelector("a")).GetAttribute("href"); 
					var image = productHTMLElement.FindElement(By.CssSelector("img")).GetAttribute("src"); 
					var name = productHTMLElement.FindElement(By.CssSelector("h2")).Text; 
					var price = productHTMLElement.FindElement(By.CssSelector(".price")).Text; 
 
					var product = new Product() { Url = url, Image = image, Name = name, Price = price }; 
 
					products.Add(product); 
				} 
			} 
 
			// export logic 
			using (var writer = new StreamWriter("products.csv")) 
			using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture)) 
			{ 
				csv.WriteRecords(products); 
			} 
		
// // initializing HAP 
// 			var web = new HtmlWeb(); 
// 			// setting a global User-Agent header 
// 			web.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/124.0.0.0 Safari/537.36"; 
// 			// creating the list that will keep the scraped data 
 
// 			var products = new List<Product>(); 
// 			// the URL of the first pagination web page 
// 			var firstPageToScrape = "https://www.scrapingcourse.com/ecommerce/page/1/"; 
// 			// the list of pages discovered during the crawling task 
// 			var pagesDiscovered = new List<string> { firstPageToScrape }; 
// 			// the list of pages that remains to be scraped 
// 			var pagesToScrape = new Queue<string>(); 
// 			// initializing the list with firstPageToScrape 
// 			pagesToScrape.Enqueue(firstPageToScrape); 
// 			// current crawling iteration 
// 			int i = 1; 
// 			// the maximum number of pages to scrape before stopping 
// 			int limit = 12; 
// 			// until there is a page to scrape or limit is hit 
// 			while (pagesToScrape.Count != 0 && i < limit) 
// 			{ 
// 				// getting the current page to scrape from the queue 
// 				var currentPage = pagesToScrape.Dequeue(); 
// 				// loading the page 
// 				var currentDocument = web.Load(currentPage); 
// 				// selecting the list of pagination HTML elements 
// 				var paginationHTMLElements = currentDocument.DocumentNode.QuerySelectorAll("a.page-numbers"); 
// 				// to avoid visiting a page twice 
// 				foreach (var paginationHTMLElement in paginationHTMLElements) 
// 				{ 
// 					// extracting the current pagination URL 
// 					var newPaginationLink = paginationHTMLElement.Attributes["href"].Value; 
// 					// if the page discovered is new 
// 					if (!pagesDiscovered.Contains(newPaginationLink)) 
// 					{ 
// 						// if the page discovered needs to be scraped 
// 						if (!pagesToScrape.Contains(newPaginationLink)) 
// 						{ 
// 							pagesToScrape.Enqueue(newPaginationLink); 
// 						} 
// 						pagesDiscovered.Add(newPaginationLink); 
// 					} 
// 				} 
// 				// getting the list of HTML product nodes 
// 				var productHTMLElements = currentDocument.DocumentNode.QuerySelectorAll("li.product"); 
// 				// iterating over the list of product HTML elements 
// 				foreach (var productHTMLElement in productHTMLElements) 
// 				{ 
// 					// scraping logic 
// 					var url = HtmlEntity.DeEntitize(productHTMLElement.QuerySelector("a").Attributes["href"].Value); 
// 					var image = HtmlEntity.DeEntitize(productHTMLElement.QuerySelector("img").Attributes["src"].Value); 
// 					var name = HtmlEntity.DeEntitize(productHTMLElement.QuerySelector("h2").InnerText); 
// 					var price = HtmlEntity.DeEntitize(productHTMLElement.QuerySelector(".price").InnerText); 
// 					var product = new Product() { Url = url, Image = image, Name = name, Price = price }; 
// 					products.Add(product); 
// 				} 
// 				// incrementing the crawling counter 
// 				i++; 
// 			} 
// 			// opening the CSV stream reader 
// 			using (var writer = new StreamWriter("products.csv")) 
// 			using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture)) 
// 			{ 
// 				// populating the CSV file 
// 				csv.WriteRecords(products); 
// 			} 
		
	
public class Product 
{ 
	public string? Url { get; set; } 
	public string? Image { get; set; } 
	public string? Name { get; set; } 
	public string? Price { get; set; } 
}




