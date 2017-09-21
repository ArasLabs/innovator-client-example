1. add the IOM reference
  a. Remove the existing IOM reference from the Solution Explorer under Solution > InnovatorClientExample.cs > References.
  b. Right click on References and select "Add Reference".
  c. Click the "Browse" button in the Reference Manager dialog.
  d. Navigate to your Innovator install directory then Innovator\Server\bin.
  e. Select IOM.dll and click "Add".
2. build the solution
3. modify file InnovatorClientExampleConfig.xml in the root project directory so that <vault>, <server> and <database> point to your installation of the Aras Innovator
4. copy the modified configuration file InnovatorClientExampleConfig.xml from the root project directory to the directory where the project executable is built
5. run the executable