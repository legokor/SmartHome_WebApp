import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './components/app/app.component';
import { NavMenuComponent } from './components/navmenu/navmenu.component';
import { HomeComponent } from './components/home/home.component';
import { CounterComponent } from './components/counter/counter.component';
import { MasterUnitManagerComponent } from './components/masterunitmanager/masterunitmanager.component';
import { MasterUnitCreatorComponenet } from './components/masterunitcreator/masterunitcreator.component';
import { MasterUnitUpdaterComponent } from './components/masterunitupdater/masterunitupdater.component';

@NgModule({
    declarations: [
        AppComponent,
        NavMenuComponent,
        CounterComponent,
        HomeComponent,
        MasterUnitManagerComponent,
        MasterUnitCreatorComponenet,
        MasterUnitUpdaterComponent
    ],
    imports: [
        CommonModule,
        HttpModule,
        FormsModule,
        RouterModule.forRoot([
            { path: '', redirectTo: 'home', pathMatch: 'full' },
            { path: 'home', component: HomeComponent },
            { path: 'counter', component: CounterComponent },
            { path: 'masterunitmanager', component: MasterUnitManagerComponent },
            { path: 'masterunitcreator', component: MasterUnitCreatorComponenet },
            { path: 'masterunitupdater/:id', component: MasterUnitUpdaterComponent },
            { path: '**', redirectTo: 'home' }
        ])
    ]
})
export class AppModuleShared {
}
