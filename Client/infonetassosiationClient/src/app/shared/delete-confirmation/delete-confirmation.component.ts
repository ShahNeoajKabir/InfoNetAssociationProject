import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-delete-confirmation',
  templateUrl: './delete-confirmation.component.html',
  styleUrls: ['./delete-confirmation.component.css']
})
export class DeleteConfirmationComponent implements OnInit {

  @Input() rowId:number | undefined;
  @Output() onConfirmedId = new EventEmitter<number>()
  constructor(
    private modalService:NgbModal,
    private toaster:ToastrService
  ) { }

  ngOnInit() {
  }

  confirmDelete(){
    this.onConfirmedId.emit(this.rowId);
    this.modalService.dismissAll();
  }

  close(){
    this.rowId = 0;
    this.modalService.dismissAll();
  }

}
