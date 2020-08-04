# Reglang
An esoteric programming Language based on Regex

Code in Reglang works by repeatedly applying regex substitutions on it's input.
The following code, given the input "Hello" would output "World!".

```regex
Hello:World!

> Hello
> World!
```

The syntax is simply:

```regex
Pattern:Substitution
Pattern:Substitution
```

ad infinitum.

If there are multiple patterns, the first one matching the current state is used, and the substitution is applied.

```regex
1:2
1:Won't be applied as the pattern above already matches

> 1
> 2
```

Afterwords the program repeats with the new state until no more patterns match.

Capture groups may also be used:

```regex
Hello (.*):Goodbye $1

> Hello World
> Goodbye World
```

# Tricks

``(?!)`` matches nothing, and as such can be used for comments:

```
(?!):Some Comment on the current political climate
```

``$^`` matches the empty string, and can be used to handle empty input:

```
$^:Please input something
```

# Example

The following example implements basic unary maths:

```
(?!):Unary Operators
$^:111+111*111^111-111^(111*111)
(1+)\^(1+)1:$1*$1^$2
(1+)\*(1+)1:$1+$1*$2
(1*)1-(1+)1:$1-$2
(1+)\+(1+):$1$2
(1+)\^1:$1
(1+)\*1:$1
(1*)1-1:$1
\((.+?)\):$1
```
