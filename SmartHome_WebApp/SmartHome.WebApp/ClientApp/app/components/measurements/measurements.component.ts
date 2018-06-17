import { Component, NgModule } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface MasterUnit {
    Id: string,
    CustomName: string,
    IsOn: boolean,
    OwnerId: string
}

@Component({
    selector: 'measurements',
    templateUrl: './masterunitmanager.component.html'
})
export class MasterUnitManagerComponent {
    public unitList: Observable<MasterUnit[]>;

    constructor(private http: HttpClient) {
        this.unitList = this.readMasterUnitList();
    }

    createMasterUnit(unit: MasterUnit): Observable<MasterUnit> {
        return this.http.post<MasterUnit>('api/v1/masterunit', unit);
    }

    readMasterUnitList(): Observable<MasterUnit[]> {
        return this.http.get<MasterUnit[]>('api/v1/masterunit');
    }

}
