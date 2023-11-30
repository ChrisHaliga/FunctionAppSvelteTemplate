<script lang="ts">
  import { onMount } from 'svelte';
  import type { IWidget } from '../services/WidgetService';
  export let data:IWidget;
  export let title:string = "";

  let element:any;

  let lastPosition = {x:null, y:null, x_beforeExpand:null, y_beforeExpand:null};
  let isDragging = false;

  $: {
    if(element){
      element.style.left = data.x + "px";
	    element.style.top = data.y + "px";
    }
  }

  function handleMousePress(e:any, isPressed:boolean){
    isDragging = isPressed;

    e.currentTarget.setAttribute('aria-grabbed', isPressed.toString());

    let toggleEventListener = isPressed ? document.addEventListener: document.removeEventListener
    toggleEventListener('mousemove', handleMouseMove, {capture: true});
    toggleEventListener('mouseup', e => handleMousePress(e, false), {capture: true});

    document.body.style.userSelect = 'none';
  }

  function handleMouseMove(e){
    if(isDragging){
      let changeX = lastPosition.x ? e.clientX - lastPosition.x : 0;
      let changeY = lastPosition.y ? e.clientY - lastPosition.y : 0;

      data.update("x", data.x + changeX);
      data.update("y", data.y + changeY);      
    }

    lastPosition.x = e.clientX;
    lastPosition.y = e.clientY;
  }

  let expanded = true;
  function onExpand(){
    if(expanded){
      lastPosition.x_beforeExpand = data.x;
      lastPosition.y_beforeExpand = data.y;

      data.update("x", 0);
      data.update("y", 0);
      element.style.right = 0;
      element.style.bottom = 0;
      element.style.width = "auto";

    }
    else{
      data.update("x", lastPosition.x_beforeExpand);
      data.update("y", lastPosition.y_beforeExpand);
      element.style.right = "auto";
      element.style.bottom = "auto";
    }
    expanded = !expanded;
  }
  
  onMount(async () => {
    if(!data.initialized){
      data.update("x", data.x - element.clientWidth/2);
      data.update("y", data.y);
      data.update("initialized", true)
    }
	});
</script>


<div class="widget"
  on:mousedown={e => handleMousePress(e, true)}
  bind:this={element}
  role="button"
  aria-grabbed="false"
  tabindex=0
>
  <div class="widget-header">
    <span class="widget-title">{title}</span>
    <div class="widget-control-panel">
      <button class="widget-control widget-minimize" on:click={e => data.onDestroy()}>
        <i class="fa-solid fa-window-minimize"></i>
      </button>
      <button class="widget-control widget-expand" on:click={e => onExpand()}>
        <i class="fa-regular fa-window-restore"></i>
      </button>
      <button class="widget-control widget-exit" on:click={e => data.onDestroy()}>
        <i class="fa-solid fa-xmark"></i>
      </button>
    </div>
  </div>
  
  <div class="widget-content">
    <slot/>
  </div>
</div>


<style lang="scss">
  .widget{
    position: absolute;
    display: flex;
    flex-direction: column;
    width: min-content;
    height: auto;
    min-width: 256px;
    max-width: 512px;
    min-height: 256px;

    background-color: rgb(196, 209, 218);
    border: 1px solid #ccc;
    border-radius: 4px;
    position:absolute;
    padding: 2px;
    
    .widget-header{
      display: flex;
      padding-left: 8px;
      align-items: center;

      .widget-title{
        white-space: nowrap;
        overflow: hidden;
        text-overflow: ellipsis;
      }
      .widget-control-panel{
        display: flex;
        flex:0;
        margin-right: 0;
        margin-left: auto;
        min-height: fit-content;
        width: min-content;

        .widget-control{
          display: flex;
          justify-content: center;
          flex:0;
          width:32px;
          height: 32px;
          margin-left: auto;
          margin-bottom: 2px;
          background-color: inherit;
          border: none;
          outline: none;
          border-radius: 0;
          transition: background-color 500ms;
          &:hover{
            background-color: rgb(157, 170, 180);
            i{
              transition: color 500ms;

              color: white;
            }
          }
        }
        .widget-exit{
          border-radius: 0 4px 0 0;
          &:hover{
            background-color: rgb(212, 57, 57);
            i{
              transition: color 500ms;

              color: white;
            }
          }
        }
      }
    }
    
    .widget-content{
      flex:1;
      background-color: #eee;
      min-width: fit-content;
      padding:8px;
    }
  }
</style>
