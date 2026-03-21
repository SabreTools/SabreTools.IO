# SabreTools.Logging

[![Build and Test](https://github.com/SabreTools/SabreTools.IO/actions/workflows/build_and_test.yml/badge.svg)](https://github.com/SabreTools/SabreTools.IO/actions/workflows/build_and_test.yml)

This library contains logic for a logging system, including writing to console and textfile outputs. There are 4 possible log levels for logging statements to be invoked with. There is also a stopwatch implementation included for logging statements with automatic timespan tracking.

## Logging Logic

Logging is performed using the `SabreTools.Logging.Logger` class, which by default allows for 4 levels of log statements:

| `LogLevel` | Description |
| --- | --- |
| `VERBOSE` | Verbose or debug logging statements that do not always need to be presented |
| `USER` | Standard logging statements that would be used during normal operation |
| `WARNING` | Non-critical issues, including missing optional parameters or obsolete behavior |
| `ERROR` | Critical issues, usually ones that prevent all operation |

All logging configuration is done with the static `SabreTools.Logging.LoggerImpl` class, which is called internally by `SabreTools.Logging.Logger`. These static configurations allow for writing to file, filtering log levels, and even throwing if an exception is logged. There is also an automatic stopwatch for total logging runtime. 
