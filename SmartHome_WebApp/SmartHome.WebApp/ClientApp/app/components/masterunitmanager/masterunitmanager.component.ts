import { Component, NgModule, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';

export interface MasterUnit {
    Id: string,
    CustomName: string,
    IsOn: boolean,
    OwnerId: string
}

@Component({
    selector: 'masterunitmanager',
    templateUrl: './masterunitmanager.component.html'
})
export class MasterUnitManagerComponent {
    public unitList: Observable<MasterUnit[]>;
    private baseUrl: string;

    constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string)
    {
        this.unitList = this.readMasterUnitList();
        this.baseUrl = baseUrl;
    }

    createMasterUnit(unit: MasterUnit): Observable<MasterUnit> {
        return this.http.post<MasterUnit>(this.baseUrl + 'api/v1/masterunit', unit);
    }

    readMasterUnitList(): Observable<MasterUnit[]> {
        return this.http.get<MasterUnit[]>(this.baseUrl + 'api/v1/masterunit');
    }

}
