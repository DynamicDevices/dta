Device Test Application
=======================

Overview
========

The DTA is a scriptable test engine which uses an SSH connection
to exercise production boards to generate a pass/fail against a
defined list of tests.

A tester is associated with a test location and a company.

Information on all tests executed by the tester is stored to a
database, which provides an audit history to be interrogated
for all shipped boards.

Unique IDs such MAC address and so forth can be stored to
the database during testing for future reference (for example
against failed boards)

In addition each board can be assigned a unique serial number
during the test process, dependent on the board hardware
supporting this and an appropriate test script element  being
created to store the serial number to the board.

Tests, boards, test sequences and so forth are all defined in the
database making the system extremely flexible.

Test Bundles / Reporting
=====================

Tests are defined for a given class of device class (board variant) 

A given device class defined in the database in turn references two resource bundles

- a script containing all tests which can possibly be executed in an XML format
- an archive containing any resources which are needed to perform the tests

The resource bundles are defined with an FTP URI. The DTA harness downloads
and caches the needed files from the configured FTP server.

In addition a URI is given to a folder on an FTP server and a report on each test
performed is uploaded to that folder upon test completion

Scripting
=======

I have attached an example script which defines a number of tests.

Each test has a name and performs a particular action or set of actions.

Files can be copied to the target board and commands executed on the target.

A usual use-case would be to execute a particular command on the target, and to parse the output
for a specific result. The test then succeeds or fails based on the presence or absence of the required
output.

There are also more custom test commands implemented, such as using variable replacement
(e.g. to define a number to call or a serial number to program)

Also messages can be displayed on-screen during the test where needed.

The test harness itself contains a set of script-classes which relate to the command types defined in the XML.

As such where a custom test behaviour is required and is not supported a new script-class can be added to
the test harness to support this behaviour.

Database test control
===================

The database contains a list of test items for each device class, with names that map to the names defined in the XML.

It also contains one or more test lists which consist of a number of the test items.

In this way it is possible to define sets of tests which run more or less of the total number of tests available.
