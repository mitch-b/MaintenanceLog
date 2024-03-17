# Data Design Thoughts

* (Phase1) `User`
* (Phase1) `Property`
  * _Typically, your House._
  * Properties contain **`Area`s** and **`Asset`s**
  * Properties:
    * **`Area`(s)**
    * **`Asset`(s)**
    * Name
    * Description
* (Phase1) `Area`
  * _ie, Garage, Kitchen_
  * Properties:
    * **`Asset`(s)**
    * AreaName
    * ...?
* (Phase1) `Asset`
  * _ie, Air Filter, Lawn Mower, Propane Tank, ..._
  * Essentially, an `Asset` is a thing you're maintaining]
  * Assets have associated **`Task`s**, **`Part`s**, **`Area`**.
  * Properties:
    * **`Area`**
    * **`TaskDefinition`(s)**
    * Name
    * Description
* (Phase1) `TaskType`
  * Repair, Maintain, Clean, _Chore_, [user-assignable]
* (Phase1) `TaskDefinition`
  * _ie, Change Oil Filter, Refill Propane Tank, ..._
  * Tasks are associated under **`Asset`s**
  * Tasks _may_ also associate with specific **`Asset`** components - **`Part`s** - that are relevant to the **TaskDefinition**.
  * Properties:
    * **`TaskType`**
    * **`Asset`(s)**
    * Task name
* (Phase1) `TaskInstance`
  * An instance of a **`TaskDefinition`**.
  * Can be auto-generated when a required lifespan hits.
  * Can be assigned to a **User**.
  * Properties:
    * **TaskDefinition**
    * **User**
    * Message (can say 'Triggered by Lifespan reached', or 'Overdue' or _something_)
    * StartedOn
    * PerformedBy (User)
    * PerformedOn (DateTime)
    * Duration (Self-Recorded)
    * DueByDate
    * MQTT-enabled (can be _Phase4_)
* (Phase2) `TaskDefinitionSteps`
  * _ie, for 'Clean Room', steps may be 'Organize books', 'Clear dresser', 'Hang clothes', etc. ..._
  * `TaskDefinitionStep`s are associated under **`TaskDefinition`s**
  * Each `TaskInstance` will have its own set of `TaskDefinitionStep`s to track progress.
  * Properties:
    * **`TaskDefinition`**
    * Task Definition Step name
    * IsOptional
* (Phase2) `TaskInstanceStep`
  * `TaskInstanceStep`s are associated under **`TaskInstance`s** and created from **`TaskDefinitionStep`s**
  * Properties:
    * **`TaskInstance`**
    * **`TaskDefinitionStep`**
    * CompletedOn
    * CompletedBy (User)
* (Phase2) `Part`
  * Parts contain `PurchasedPart`s
  * Properties:
    * **`Asset`**
    * **`PurchasedPart`(s)**
    * Name
    * Image
    * IsConsumable? (if yes, needs lifespan)
    * Lifespan/Needs Replacement After (AI derived?)
* (Phase3) `PurchasedPart`
  * PurchaseHistory is an instance of a new **Part** acquisition
  * Properties:
    * URL
    * Cost
    * PurchasedOn
    * Notes
    * Vendor/Store
* Notification?
* Schedule?
  * Automatically create a **TaskInstance** when a **TaskDefinition** is due.
  * Automatically create a **TaskInstance** when a **Part** is due for replacement.
* User-Defined Fields
  * Installations can define custom fields for:
    * **Property**
    * **Area**
    * **Asset**
    * **TaskDefinition**
    * **TaskInstance**
    * **Part**
    * **PurchasedPart**
