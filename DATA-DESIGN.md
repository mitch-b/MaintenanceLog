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
    * **`Task`(s)**
    * Name
    * Description
* (Phase1) `TaskType`
  * Repair, Maintain, Clean, _Chore_, [user-assignable]
* (Phase1) `Task`
  * _ie, Change Oil Filter, Refill Propane Tank, ..._
  * Tasks are associated under **`Asset`s**
  * Tasks _may_ also associate with specific **`Asset`** components - **`Part`s** - that are relevant to the **Task**.
  * Properties:
    * **`TaskType`**
    * **`Asset`(s)**
    * Task name
    * Task system-name (kebab-name but overridable)
    * MQTT-enabled (can be _Phase4_)
    * PerformedBy (User)
    * PerformedOn (DateTime)
    * Duration (Self-Recorded)
* (Phase1) `TaskInstance`
  * An instance of a **`Task`**.
  * Can be auto-generated when a required lifespan hits.
  * Can be assigned to a **User**.
  * Properties:
    * **Task**
    * **User**
    * Message (can say 'Triggered by Lifespan reached', or 'Overdue' or _something_)
    * StartedOn
    * DueByDate
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