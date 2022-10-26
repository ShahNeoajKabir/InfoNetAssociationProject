import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-image-control',
  templateUrl: './image-control.component.html',
  styleUrls: ['./image-control.component.css']
})
export class ImageControlComponent implements OnInit {

  constructor() { }

  ngOnInit() {
  }

  @Input() colSize:string = 'col-sm-8';
  @Input() fieldId:string='';
  @Input() isRequired:boolean=false;
  @Input() previewImg:string = '';
  @Output() selectedFile = new EventEmitter<any>();
  @Output() getFileOnChange = new EventEmitter<any>();


  onImageChange(files: FileList) {
    if (files.length == 0) { return null; }
    //Show image
    if(this.validateFileExtension(files[0].name.split('.').pop())){
      var readerForShow = new FileReader();
      readerForShow.onload = (event: any) => {
        this.previewImg = event.target.result;
      }
      readerForShow.readAsDataURL(files.item(0)!);
      //Convert image
      var readerForConvert = new FileReader();
      readerForConvert.onload = this._handleReaderLoaded.bind(this);
      readerForConvert.readAsBinaryString(files[0])
      document.getElementById(this.fieldId)!.innerText = files[0].name;

      return readerForConvert
    }
    else{
      return false
    }

  }

  onChangeFile(files:FileList){
    console.log(files)

    if (files.length == 0) { return null; }
    debugger
    if(this.validateFileExtension(files[0].name.split('.').pop())){
      var readerForShow = new FileReader();
      readerForShow.onload = (event: any) => {
        this.previewImg = event.target.result;
      }
      this.getFileOnChange.emit(files.item(0))
      return readerForShow;
    }
    else{
      return false;
    }


  }
  validateFileExtension(file: string | undefined) {
    if (file === 'docx' || file === 'pdf') return true;
    else return false;
  }
  _handleReaderLoaded(readerEvt:any) {
    var binaryString = readerEvt.target.result;
    this.previewImg=btoa(binaryString);  // Converting file to binary string data.
    this.selectedFile.emit(btoa(binaryString));  // Converting file to binary string data.
  }

  clearPicture() {
    this.previewImg='';
    this.selectedFile.emit(null);
    document.getElementById(this.fieldId)!.innerText = 'Choose file...'
  }

}

