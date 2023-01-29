TranslationSystem
=================

This is a Telegram bot for learning English vocabulary made using C#. It has various commands that allow users to add new words, view all added words, show a specific word, and enter quiz mode.

Command List
------------

1.  `/add (word)` - add a new word to the vocabulary list.
2.  `/showall` - display all words that have been added to the vocabulary list.
3.  `/show (word)` - show a specific word and its definition.
4.  `/next` - enter quiz mode and test your vocabulary knowledge.

Custom Command Handlers
-----------------------

The TranslationSystem uses a custom mechanism for resolving command handlers based on the specific command received as a message. This system also includes full support for dependency injection, making it easy to extend and maintain the codebase.

Built With
----------

*   C# 6
*   MongoDb
*   OpenAi

OpenAi Integration
------------------

The TranslationSystem utilizes OpenAi models to generate definitions and translations for the words added to the vocabulary list. This allows for accurate and reliable information to be provided to users.
