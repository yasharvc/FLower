# Flower
This is a simple ***Flow***-mak***er*** that is based on simply algorithm:

## Process
We have everything encapsulated in `Process` that is contains start and end `stage`:
| start         | end         |
| ------------- | ----------- |
| begin `stage` | end `stage` |

this is the basic structure of a `Process`.

### Add new `Stage` in between two `Stage`s
 If we want to add new `stage` between two given `stage`, every stage have both just one `Previous` and **List** of `Stage` for `Next` stage.