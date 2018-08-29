import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Data } from '@angular/router';

@Component({
  selector: 'app-site-data',
  templateUrl: './site-data.component.html',
  styleUrls: ['./site-data.component.css']
})
export class SiteDataComponent {
  public http: HttpClient;
  public baseUrl: string;
  public contracts: Contract[];

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.http = http;
    this.baseUrl = baseUrl;

    this.http.get<Contract[]>(this.baseUrl + 'api/Contracts').subscribe(result => {
      this.contracts = result;
    }, error => console.error(error));
  }

  selectedContract: Contract;
  componentItemsPages: number;
  componentItemsPageNum: number = -1;
  selectSite(contractId: number) {
    this.selectedContract = this.contracts.find(contract => contract.contractId === contractId);
    this.getContractComponentItems();
  }
  getContractComponentItems() {
    if (this.componentItemsPageNum === -1) {
      this.http.get<number>(this.baseUrl + 'api/ComponentItems/GetComponentItemsByContract?contractNo=' + this.selectedContract.contractNo + '&getTotalPages=true').subscribe(result => {
        this.componentItemsPages = result;
        this.componentItemsPageNum = 1;
        this.getContractComponentItemsData();
      }, error => console.error(error));
    } else {
      this.getContractComponentItemsData();
    }
  }
  getContractComponentItemsData() {
    this.http.get<ComponentItem[]>(this.baseUrl + 'api/ComponentItems/GetComponentItemsByContract?contractNo=' + this.selectedContract.contractNo + '&pageNum=' + this.componentItemsPageNum).subscribe(result => {
      this.selectedContract.componentItems = result;
    }, error => console.error(error));
  }
}

interface Contract {
  contractId: number;
  contractNo: string;
  contractCode: string;
  description: string;
  componentItems: ComponentItem[];
}

interface ComponentItem {
  componentItemId: number;
  componentId: number;
  serialNo: string;
  tagId: string;
  wing: string;
  block: string;
  floor: string;
  flat: string;
  inspectionGrading: string;
  remark: string;
  taggingTimestamp: Date;
  inspectionTimestamp: Date;
  manufacturingTimestamp: Date;
  deliveryTimestamp: Date;
  receivingTimestamp: Date;
  installationTimestamp: Date;
}
