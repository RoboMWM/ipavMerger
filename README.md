# ipavMerger
(I should rename this but I'm too lazy to right now. Maybe after I rewrite it a couple years later)

So initially I was going to make this to merge two data files of the ipav plugin but I ended up deciding not to do that for whatever reason.

Instead, it merges (some) data in itself and then allows you to view relevant information (aliases, name changes).

# How to use

1. Run executable with arguments of the two directories containing the ipav files. (This utility will not alter these files.)
Optionally, if you'd like to see which players changed their name , add a number as a third argument, with the number representing the minimum names that player used.

2. ipavMerger will print aliases based on UUIDs (and name changes, if you specified a desire for this)

3. ipavMerger will ask if you want to search for an alias. If so, enter a UUID.

4. To quit, press CTRL-C or close the command prompt.
