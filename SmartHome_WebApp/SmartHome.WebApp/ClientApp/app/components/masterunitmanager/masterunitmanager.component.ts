import { Component, NgModule, Inject } from '@angular/core';
import { Http } from '@angular/http';
import 'rxjs/add/operator/map';
import { Observable } from 'rxjs';
import { MasterUnit } from '../MasterUnit';


@Component({
    selector: 'masterunitmanager',
    templateUrl: './masterunitmanager.component.html'
})
export class MasterUnitManagerComponent {
    public unitList: MasterUnit[] = [];
    private baseUrl: string;

    constructor(private http: Http, @Inject('BASE_URL') baseUrl: string)
    {
        this.baseUrl = baseUrl;
        this.readMasterUnitList();
    }

    readMasterUnitList(): void {
        let debug = this.unitList;
        this.http.get(this.baseUrl + 'api/v1/masterunit')
            .map((response) => response.json())
            .subscribe(
            (data) => {
                let castedData = data as Array<MasterUnit>;
                this.unitList = castedData.map(map => {
                    return new MasterUnit(map.id, map.eTag, map.customName, map.isOn, map.ownerId);
                });
                return;
            },
                error => alert(error)
        );
    }

}
