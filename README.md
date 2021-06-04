# BrainFix
BrainF#ck with additional features that makes it easier, which eliminates the entire point of the BrainF#ck language.. :upside_down_face:
Cell values are stored as char and the value range is between Char.MinValue and Char.MaxValue, not 0 and 255. Therefore it does not work well for now

### Basic Commands
1. `>` Go to next cell
2. `<` Go to previous cell
3. `+` Add 1 to current cell
4. `-` Subtract 1 from current cell
5. `M` Max the value of the current cell
6. `O` Zero the value of the current cell
7. `.` Print the current cell value as ascii
8. `,` Take input from the user

### If|else
Basic syntax of an if statement:
`[A Comparison Symbol][Number]?(execute here if so)|(executed here if not so)K` You must put K at the end of an if-else statement. |(pipe) seperates the if and else part.
i.e. `V10?-|+K` if value of the current cell is less than 10, subtract it by one (-). Else(|) add one to it (+). And end the if-else statement (K)
##### Comparison Symbols
1. `^`: Greater than
2. `V`: Less than
3. `=`: Equal to

### Debug Commands
1. `@*`: Prints the cell no
2. `@.`: Prints the cell value
3. `@Ğ`: :man_facepalming:

### Comments
Anything between two `#` are not executed. `#This is a comment#`