echo off
setlocal EnableDelayedExpansion

IF "%1"=="-f" (
set filterfilename=filter.txt
IF NOT "%2"=="" set filterfilename=%2
 
set var=
FOR /f "delims=" %%G in (!filterfilename!) DO (
	SET _filter=%%G
	SET firsttwo=!_filter:~0,2!
	IF NOT "!firsttwo!"=="--" set var=!var!,%%G
)
set var=!var:~1!
dotnet vstest AlphaPoint_QA.dll  --logger:"nunit;LogFileName=nunit_file_name.trx;verbosity=detailed" --ResultsDirectory:"Results" /Tests:!var!
)

IF NOT "%1"=="-f" dotnet vstest AlphaPoint_QA.dll  --logger:"nunit;LogFileName=nunit_file_name.trx;verbosity=detailed" --ResultsDirectory:"Results" 

echo Test Case Execution Finished...
echo Genrating Reports Now....
GenrateReport.bat




