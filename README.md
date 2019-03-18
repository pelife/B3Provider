## B3PRovider
This project is intended to provide a way to access public availabe data of the B3 Stock Exchange (former BMF & Bovespa).

## Motivation
Try to provide a little organization to the mess of all sorts of files into a generic and comprehensive framework to be incorporated into client apllications of all sorts.

## Code style
Please keep the observed current code style. Don't use your creativity here, put it somewhere else. I am sure you will find it pretty easy to follow.

## Screenshots
### B3 Explorer
B3 Explorer is a project to allow the user of B3 provider to visualize the data it makes available.

![Options screen example](/img/B3_Explorer1.png?raw=true "B3 Explorer Options Screen")
![Stocks screen example](/img/B3_Explorer2.png?raw=true "B3 Explorer Stocks Screen")
![Futures screen example](/img/B3_Explorer3.png?raw=true "B3 Explorer Futures Screen")

      

## Tech/framework used
For now N/A
But probably will use:
- Flat files reading framework: <a href='https://github.com/forcewake/FlatFile'>FlatFile</a>
- Excel files reading framework: <a href='https://github.com/ExcelDataReader/ExcelDataReader'>ExcelDataReader</a>
- Logging framework

## Features

- [x] Equity Instruments Loading
- [x] Options on Equities Instruments Loading
- [x] Loading Daily Quotes Files
- [x] Loading Historic Quotes Files
- [x] Company Sector Classification Files
- [ ] Loading Indexes Files
- [ ] Loading Future Data
- [ ] Loading Margin Call Files
- [ ] Loading Holiday Files (already in source code)
- [ ] Include columns of price movement (1Day, WTD, MTD, YTD)(on progress)
- [ ] Include columns of indicators (avg volume, atr, irf, mas)
- [ ] Smart downloads (only when needed)
- [ ] Save info to database
- [ ] Load info from database
- [ ] Support to asynchronous operations

Infelizmente não consigo mais encontrar a documentação do arquivo no site oficial da B3 ou IPN. 
Obrigado ao genio que publicou em um site alternativo:
[Manual de mensagens](https://manualzz.com/doc/5991070/cat%C3%A1logo-de-mensagens-cadastro-de-instrumento)

## Code Example

```csharp
// create a configuration instance
var config = new B3ProviderConfig();

// define properties
config.ReplaceExistingFiles = true;

// create an instance of the client
var client = new B3ProviderClient(config);

// load all instruments into memory
client.LoadInstruments();

// get information about PETR4 stock (the most popular in B3)
var equity = client.EquityInstruments.Where(e => e.Ticker.Equals("PETR4", StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();

// get information about option calls on PETR4 stock 
var optionsCalls = client.OptionInstruments.Where(o => o.B3IDUnderlying == equity.B3ID && o.Type == B3OptionOnEquityTypeInfo.Call).ToList();

// get information about option puts on PETR4 stock 
var optionsPuts = client.OptionInstruments.Where(o => o.B3IDUnderlying == equity.B3ID && o.Type == B3OptionOnEquityTypeInfo.Put).ToList();
```

## Contribute
Anyone who whishs to contribute to the project just have to remember to follow the rules accordingly to the following guide [contributing guideline](https://github.com/zulip/zulip-electron/blob/master/CONTRIBUTING.md) will be a big plus.

## Credits
- This project was inpired by a fellow risk manager <a href='https://github.com/wilsonfreitas'>Wilson Freitas</a> interested in programming that created a similar work for people who uses R Language in data analysis.
His project is available in <a href='https://gist.github.com/wilsonfreitas/a875444ac3d838486add6cb05261f826'>extract.r</a>

- In the source code you will find references to all relevant people that directly or indirectly contributed to the final results.

## License
B3Provider is available on github <a href='https://github.com/pelife/prototyping'>here</a>
under <a href='https://github.com/pelife/prototyping/blob/master/MIT-LICENSE.txt'>MIT license</a>.
If you hit bugs, fill issues on github.
Feel free to fork, modify and have fun with it :)


MIT © [Felipe Bahiana Almeida](https://www.linkedin.com/in/felipe-almeida-ba222577/)
