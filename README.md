# SomeoneCaresHostsFileUpdater

This program automatically updates your hosts file with the one maintained
at https://someonewhocares.org/. For those unaware with SomeoneWhoCares, it is a 
website by Dan Pollock. Its goal is to provide a hosts file that protects a computer
from many types of spyware, reduce bandwith use, block pop-ups, removes user tracking 
and blocks most advertising on the internet. 

## Getting Started

### For Developers
In order to get a local version of the project up and running click on the clone or download 
button to download the files. Once imported, a dotnet restore may be required. 

### Prerequisites

To install the software, navigate to the downloads directory and extract the build file. 

It needs to be ran in adminstrator mode and the equivalent of Dotnet Framework 4.7.1 installed. 

## Running the tests

This solution uses the NUnit framework and Moq to initiate tests. Visual Studio 
offers a test explorer and a tests menu. To run them open the test explorer and 
click run all. 

### Break down into end to end tests

So far the only test written is one to determine that the program 
is able to contact with the website hosting the hosts file, to ensure
that no invalid responses will be given. Additional tests ensuring that
the correct location of the hosts file will be created at a later date. 

## Built With

* [Moq](https://github.com/Moq/moq4/wiki) - The testing framework used

## Authors


## License

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details

## Acknowledgments

* Dan Pollock for creating the SomeoneWhoCares web site and hosts file. 
