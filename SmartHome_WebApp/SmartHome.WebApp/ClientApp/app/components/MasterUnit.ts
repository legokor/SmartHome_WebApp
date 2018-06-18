export class MasterUnit {
    id: string = "00000000-0000-0000-0000-000000000000";
    eTag: string = "00000000-0000-0000-0000-000000000000";
    customName: string = "";
    isOn: boolean = false;
    ownerId: string = "00000000-0000-0000-0000-000000000000"
    constructor(id: string = "00000000-0000-0000-0000-000000000000",
        etag: string = "00000000-0000-0000-0000-000000000000",
        customName: string = "",
        isOn: boolean = false,
        ownerId: string = "00000000-0000-0000-0000-000000000000")
    {
        this.id = id;
        this.eTag = etag;
        this.customName = customName;
        this.isOn = isOn;
        this.ownerId = ownerId;
    }
}
