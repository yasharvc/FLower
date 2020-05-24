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

### `StatusEnum`:

    - `Idle`: the object is not touched yet and waiting for flow to touch
    - `InProgress`: the object get touch of flow and in progress
    - `Reject`: the result of object is reject.
    - `Complete`: the result of object is complete and flow can continue into next step.
  
> [!NOTE]
> Proces => StartStage => Intermediate stages => End Stage
When we clone a process then we have following data copied:
> [!IMPORTANT]
> ProcessResult => StartStageResult => Intermediate stagesresults => End StageResult
