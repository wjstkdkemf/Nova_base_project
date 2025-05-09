function wait_fence()
    while __Nova.coroutineHelper.fence == nil do
        coroutine.step()
    end
    return __Nova.coroutineHelper:TakeFence()
end

function signal_fence(value)
    __Nova.coroutineHelper:SignalFence(value)
end

local function check_lazy_before(name)
    if __Nova.executionContext.mode ~= Nova.ExecutionMode.Lazy then
        error(name .. ' should only be called in lazy execution blocks')
        return false
    end
    if __Nova.executionContext.stage ~= Nova.DialogueActionStage.BeforeCheckpoint then
        error(name .. ' should only be called in BeforeCheckpoint stage')
        return false
    end
    return true
end

function minigame(prefab_loader, prefab_name)
    if not check_lazy_before('minigame') then
        return
    end

    stop_auto_ff()
    input_off()
    box_hide()
    __Nova.coroutineHelper:StartInterrupt()

    show(prefab_loader, prefab_name)
    wait_fence()
    hide(prefab_loader)

    __Nova.coroutineHelper:StopInterrupt()
    box_show()
    input_on()
end
Nova.DialogueEntryPreprocessor.AddCheckpointNextPattern('minigame', 'ensure_ckpt_on_next_dialogue')
